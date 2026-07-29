using GestionNotes.Core.Models;

namespace GestionNotes.Core.Stores;

public interface IEleveStore
{
    Task<EleveModel> CreateAsync(EleveModel model);
    Task<PagedResult<EleveModel>> GetAllAsync(int page, int pageSize);
    Task<EleveModel?> GetByUserIdAsync(Guid userId);
    Task<EleveModel?> UpdateAsync(Guid userId, EleveModel model);
    Task<bool> DeleteAsync(Guid userId);
}
