using GestionNotes.Core.Models;
using GestionNotes.Core.Results;

namespace GestionNotes.Core.Services;

public interface IEleveService
{
    Task<Result<EleveModel>> CreateAsync(RegisterRequest request);
    Task<Result<PagedResult<EleveModel>>> GetAllAsync(int page = 1, int pageSize = 20);
    Task<Result<EleveModel>> GetByUserIdAsync(Guid userId);
    Task<Result<EleveModel>> UpdateAsync(Guid userId, EleveModel model);
    Task<Result> DeleteAsync(Guid userId);
}
