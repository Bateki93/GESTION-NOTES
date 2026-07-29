using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace GestionNotes.Blazor.Auth;

/// <summary>
/// Ce handler s'ajoute à toutes les requêtes HTTP vers l'API.
/// Il récupère le token JWT depuis localStorage et l'ajoute dans l'en-tête "Authorization".
/// Comme ça, tous les appels API sont automatiquement authentifiés.
/// </summary>
public class AuthMessageHandler : DelegatingHandler
{
    private readonly IJSRuntime _js;

    public AuthMessageHandler(IJSRuntime js)
    {
        _js = js;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _js.InvokeAsync<string>("localStorage.getItem", "authToken");
        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        return await base.SendAsync(request, cancellationToken);
    }
}
