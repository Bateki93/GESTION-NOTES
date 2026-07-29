using System.Net.Http.Json;
using GestionNotes.Blazor.Models;

namespace GestionNotes.Blazor.Services;

/// <summary>
/// Appels API pour la gestion des matières.
/// </summary>
public class MatiereService
{
    private readonly HttpClient _http;

    public MatiereService(HttpClient http) => _http = http;

    public async Task<List<MatiereModel>> GetAllAsync()
    {
        return await _http.GetFromJsonAsync<List<MatiereModel>>("api/matieres") ?? [];
    }

    public async Task<MatiereModel?> CreateAsync(MatiereModel matiere)
    {
        var response = await _http.PostAsJsonAsync("api/matieres", matiere);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<MatiereModel>();
    }

    public async Task<MatiereModel?> UpdateAsync(int id, MatiereModel matiere)
    {
        var response = await _http.PutAsJsonAsync($"api/matieres/{id}", matiere);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<MatiereModel>();
    }

    public async Task DeleteAsync(int id)
    {
        await _http.DeleteAsync($"api/matieres/{id}");
    }
}
