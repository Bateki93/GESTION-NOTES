using GestionNotes.Core.Models;

namespace GestionNotes.Core.Stores;

public interface IMatiereStore
{
    Task<MatiereModel> CreateAsync(MatiereModel model);
    Task<List<MatiereModel>> GetAllAsync();
    Task<MatiereModel?> GetByIdAsync(int id);
    Task<MatiereModel?> UpdateAsync(int id, MatiereModel model);
    Task<bool> DeleteAsync(int id);
}
