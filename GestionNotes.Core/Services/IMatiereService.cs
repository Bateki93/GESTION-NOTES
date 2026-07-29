using GestionNotes.Core.Models;
using GestionNotes.Core.Results;

namespace GestionNotes.Core.Services;

public interface IMatiereService
{
    Task<Result<MatiereModel>> CreateAsync(MatiereModel model);
    Task<Result<List<MatiereModel>>> GetAllAsync();
    Task<Result<MatiereModel>> UpdateAsync(int id, MatiereModel model);
    Task<Result> DeleteAsync(int id);
}
