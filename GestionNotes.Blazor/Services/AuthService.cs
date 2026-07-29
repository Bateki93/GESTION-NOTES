using System.Net.Http.Json;
using GestionNotes.Blazor.Auth;
using GestionNotes.Blazor.Models;
using Microsoft.JSInterop;

namespace GestionNotes.Blazor.Services;

/// <summary>
/// Gère l'inscription, la connexion et la déconnexion.
/// Stocke le token JWT dans localStorage pour le conserver entre les sessions.
/// </summary>
public class AuthService
{
    private readonly HttpClient _http;
    private readonly IJSRuntime _js;
    private readonly JwtAuthStateProvider _authStateProvider;

    public AuthService(HttpClient http, IJSRuntime js, JwtAuthStateProvider authStateProvider)
    {
        _http = http;
        _js = js;
        _authStateProvider = authStateProvider;
    }

    public async Task<AuthResponse?> LoginAsync(string email, string password)
    {
        var response = await _http.PostAsJsonAsync("api/auth/login", new LoginRequest
        {
            Email = email,
            Password = password
        });

        if (!response.IsSuccessStatusCode) return null;

        var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
        if (result is not null)
        {
            await _js.InvokeVoidAsync("localStorage.setItem", "authToken", result.Token);
            await _js.InvokeVoidAsync("localStorage.setItem", "userId", result.UserId.ToString());
            await _js.InvokeVoidAsync("localStorage.setItem", "userRole", result.RoleId.ToString());
            await _js.InvokeVoidAsync("localStorage.setItem", "userEmail", result.Email);
            _authStateProvider.NotifyUserLoggedIn();
        }
        return result;
    }

    public async Task<AuthResponse?> RegisterAsync(string email, string password, string nom, string prenom, int roleId)
    {
        var response = await _http.PostAsJsonAsync("api/auth/register", new RegisterRequest
        {
            Email = email,
            Password = password,
            Nom = nom,
            Prenom = prenom,
            RoleId = roleId
        });

        if (!response.IsSuccessStatusCode) return null;

        var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
        if (result is not null)
        {
            await _js.InvokeVoidAsync("localStorage.setItem", "authToken", result.Token);
            await _js.InvokeVoidAsync("localStorage.setItem", "userId", result.UserId.ToString());
            await _js.InvokeVoidAsync("localStorage.setItem", "userRole", result.RoleId.ToString());
            await _js.InvokeVoidAsync("localStorage.setItem", "userEmail", result.Email);
            _authStateProvider.NotifyUserLoggedIn();
        }
        return result;
    }

    public async Task LogoutAsync()
    {
        await _js.InvokeVoidAsync("localStorage.removeItem", "authToken");
        await _js.InvokeVoidAsync("localStorage.removeItem", "userId");
        await _js.InvokeVoidAsync("localStorage.removeItem", "userRole");
        await _js.InvokeVoidAsync("localStorage.removeItem", "userEmail");
        _authStateProvider.NotifyUserLoggedOut();
    }

    public async Task<string?> GetTokenAsync()
    {
        return await _js.InvokeAsync<string>("localStorage.getItem", "authToken");
    }

    public async Task<int> GetRoleAsync()
    {
        var role = await _js.InvokeAsync<string>("localStorage.getItem", "userRole");
        return int.TryParse(role, out var r) ? r : 0;
    }

    public async Task<Guid> GetUserIdAsync()
    {
        var id = await _js.InvokeAsync<string>("localStorage.getItem", "userId");
        return Guid.TryParse(id, out var uid) ? uid : Guid.Empty;
    }
}
