using GestionNotes.Core.Constants;
using GestionNotes.Core.Models;
using GestionNotes.Core.Results;
using GestionNotes.Core.Services;
using GestionNotes.Core.Stores;

namespace GestionNotes.Application.Services;

public class NoteService : INoteService
{
    private readonly INoteStore _noteStore;

    public NoteService(INoteStore noteStore)
    {
        _noteStore = noteStore;
    }

    public async Task<Result<NoteModel>> CreateAsync(NoteModel model)
    {
        var note = await _noteStore.CreateAsync(model);
        return Result<NoteModel>.Ok(note);
    }

    public async Task<Result<PagedResult<NoteModel>>> GetAllAsync(int page = 1, int pageSize = 20)
    {
        var result = await _noteStore.GetAllAsync(page, pageSize);
        return Result<PagedResult<NoteModel>>.Ok(result);
    }

    public async Task<Result<List<NoteModel>>> GetByEleveIdAsync(Guid eleveId, Guid currentUserId, int currentRoleId)
    {
        if (currentRoleId != 1 && eleveId != currentUserId)
            return Result<List<NoteModel>>.Fail(ErrorMessages.NotYourNotes);

        var notes = await _noteStore.GetByEleveIdAsync(eleveId);
        return Result<List<NoteModel>>.Ok(notes);
    }

    public async Task<Result<NoteModel>> GetByIdAsync(int id)
    {
        var note = await _noteStore.GetByIdAsync(id);
        if (note is null)
            return Result<NoteModel>.Fail(ErrorMessages.NoteNotFound);

        return Result<NoteModel>.Ok(note);
    }

    public async Task<Result<NoteModel>> UpdateAsync(int id, NoteModel model)
    {
        var updated = await _noteStore.UpdateAsync(id, model);
        if (updated is null)
            return Result<NoteModel>.Fail(ErrorMessages.NoteNotFound);

        return Result<NoteModel>.Ok(updated);
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var deleted = await _noteStore.DeleteAsync(id);
        if (!deleted)
            return Result.Fail(ErrorMessages.NoteNotFound);

        return Result.Ok();
    }
}
