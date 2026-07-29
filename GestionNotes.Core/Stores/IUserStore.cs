using GestionNotes.Core.Models;

namespace GestionNotes.Core.Stores;

public interface IUserStore
{
    Task<UserModel?> GetByIdAsync(Guid id);
    Task<UserModel?> GetByEmailAsync(string email);
    Task<UserModel> CreateAsync(UserModel model, string passwordHash);
    Task<string?> GetPasswordHashAsync(Guid userId);
}
