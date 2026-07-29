using System.Net.Http.Json;
using GestionNotes.Blazor.Models;

namespace GestionNotes.Blazor.Services;

/// <summary>
/// Appels API pour la gestion des élèves.
/// </summary>
public class EleveService
{
    private readonly HttpClient _http;

    public EleveService(HttpClient http) => _http = http;

    public async Task<PagedResult<EleveModel>> GetAllAsync(int page = 1, int pageSize = 20)
    {
        return await _http.GetFromJsonAsync<PagedResult<EleveModel>>($"api/eleves?page={page}&pageSize={pageSize}")
            ?? new PagedResult<EleveModel>();
    }

    public async Task<EleveModel?> GetByUserIdAsync(Guid userId)
    {
        return await _http.GetFromJsonAsync<EleveModel>($"api/eleves/{userId}");
    }

    public async Task<EleveModel?> UpdateAsync(Guid userId, EleveModel eleve)
    {
        var response = await _http.PutAsJsonAsync($"api/eleves/{userId}", eleve);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<EleveModel>();
    }

    public async Task DeleteAsync(Guid userId)
    {
        await _http.DeleteAsync($"api/eleves/{userId}");
    }
}
