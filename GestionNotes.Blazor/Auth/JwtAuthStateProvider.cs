using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace GestionNotes.Blazor.Auth;

/// <summary>
/// Blazor a besoin de savoir si l'utilisateur est connecté ou non.
/// Ce service lit le token depuis localStorage et crée une "identité" (ClaimsPrincipal).
/// Si le token existe, l'utilisateur est considéré connecté.
/// Utilisé par les composants AuthorizeView et [Authorize] dans les pages.
/// </summary>
public class JwtAuthStateProvider : AuthenticationStateProvider
{
    private readonly IJSRuntime _js;

    public JwtAuthStateProvider(IJSRuntime js)
    {
        _js = js;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _js.InvokeAsync<string>("localStorage.getItem", "authToken");

        if (string.IsNullOrEmpty(token))
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        var claims = ParseClaimsFromJwt(token);
        var identity = new ClaimsIdentity(claims, "jwt");
        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    public void NotifyUserLoggedIn()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public void NotifyUserLoggedOut()
    {
        NotifyAuthenticationStateChanged(Task.FromResult(
            new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))));
    }

    /// <summary>
    /// Lit les claims (informations) depuis le token JWT sans le valider.
    /// On a besoin du UserId (NameIdentifier) et du RoleId (role).
    /// </summary>
    private static List<Claim> ParseClaimsFromJwt(string jwt)
    {
        var claims = new List<Claim>();
        var payload = jwt.Split('.')[1];
        var jsonBytes = Base64UrlDecode(payload);
        var keyValuePairs = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        if (keyValuePairs is null) return claims;

        // Map the claim types to the standard ones Blazor utilise
        if (keyValuePairs.TryGetValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", out var userId))
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userId.ToString()!));

        if (keyValuePairs.TryGetValue("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", out var role))
            claims.Add(new Claim(ClaimTypes.Role, role.ToString()!));

        if (keyValuePairs.TryGetValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", out var email))
            claims.Add(new Claim(ClaimTypes.Email, email.ToString()!));

        return claims;
    }

    private static byte[] Base64UrlDecode(string input)
    {
        var base64 = input.Replace('-', '+').Replace('_', '/');
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}
