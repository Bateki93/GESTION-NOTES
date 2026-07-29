using System.Net.Http.Json;
using GestionNotes.Blazor.Models;

namespace GestionNotes.Blazor.Services;

/// <summary>
/// Appels API pour la gestion des notes.
/// </summary>
public class NoteService
{
    private readonly HttpClient _http;

    public NoteService(HttpClient http) => _http = http;

    public async Task<PagedResult<NoteModel>> GetAllAsync(int page = 1, int pageSize = 20)
    {
        return await _http.GetFromJsonAsync<PagedResult<NoteModel>>($"api/notes?page={page}&pageSize={pageSize}")
            ?? new PagedResult<NoteModel>();
    }

    public async Task<List<NoteModel>> GetByEleveIdAsync(Guid eleveId)
    {
        return await _http.GetFromJsonAsync<List<NoteModel>>($"api/notes/eleve/{eleveId}") ?? [];
    }

    public async Task<NoteModel?> GetByIdAsync(int id)
    {
        return await _http.GetFromJsonAsync<NoteModel>($"api/notes/{id}");
    }

    public async Task<NoteModel?> CreateAsync(NoteModel note)
    {
        var response = await _http.PostAsJsonAsync("api/notes", note);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<NoteModel>();
    }

    public async Task<NoteModel?> UpdateAsync(int id, NoteModel note)
    {
        var response = await _http.PutAsJsonAsync($"api/notes/{id}", note);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<NoteModel>();
    }

    public async Task DeleteAsync(int id)
    {
        await _http.DeleteAsync($"api/notes/{id}");
    }
}
