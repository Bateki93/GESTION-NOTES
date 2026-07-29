using GestionNotes.Core.Models;

namespace GestionNotes.Core.Stores;

public interface INoteStore
{
    Task<NoteModel> CreateAsync(NoteModel model);
    Task<PagedResult<NoteModel>> GetAllAsync(int page, int pageSize);
    Task<List<NoteModel>> GetByEleveIdAsync(Guid eleveId);
    Task<NoteModel?> GetByIdAsync(int id);
    Task<NoteModel?> UpdateAsync(int id, NoteModel model);
    Task<bool> DeleteAsync(int id);
}
