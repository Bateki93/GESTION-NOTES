using GestionNotes.Core.Constants;
using GestionNotes.Core.Models;
using GestionNotes.Core.Results;
using GestionNotes.Core.Services;
using GestionNotes.Core.Stores;

namespace GestionNotes.Application.Services;

public class MatiereService : IMatiereService
{
    private readonly IMatiereStore _matiereStore;

    public MatiereService(IMatiereStore matiereStore)
    {
        _matiereStore = matiereStore;
    }

    public async Task<Result<MatiereModel>> CreateAsync(MatiereModel model)
    {
        var matiere = await _matiereStore.CreateAsync(model);
        return Result<MatiereModel>.Ok(matiere);
    }

    public async Task<Result<List<MatiereModel>>> GetAllAsync()
    {
        var matieres = await _matiereStore.GetAllAsync();
        return Result<List<MatiereModel>>.Ok(matieres);
    }

    public async Task<Result<MatiereModel>> UpdateAsync(int id, MatiereModel model)
    {
        var updated = await _matiereStore.UpdateAsync(id, model);
        if (updated is null)
            return Result<MatiereModel>.Fail(ErrorMessages.MatiereNotFound);

        return Result<MatiereModel>.Ok(updated);
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var deleted = await _matiereStore.DeleteAsync(id);
        if (!deleted)
            return Result.Fail(ErrorMessages.MatiereNotFound);

        return Result.Ok();
    }
}
