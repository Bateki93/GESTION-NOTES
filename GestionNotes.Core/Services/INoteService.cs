using GestionNotes.Core.Models;
using GestionNotes.Core.Results;

namespace GestionNotes.Core.Services;

public interface INoteService
{
    Task<Result<NoteModel>> CreateAsync(NoteModel model);
    Task<Result<PagedResult<NoteModel>>> GetAllAsync(int page = 1, int pageSize = 20);
    Task<Result<List<NoteModel>>> GetByEleveIdAsync(Guid eleveId, Guid currentUserId, int currentRoleId);
    Task<Result<NoteModel>> GetByIdAsync(int id);
    Task<Result<NoteModel>> UpdateAsync(int id, NoteModel model);
    Task<Result> DeleteAsync(int id);
}
