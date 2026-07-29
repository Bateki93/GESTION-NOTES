using GestionNotes.Core.Models;
using GestionNotes.Core.Results;

namespace GestionNotes.Core.Services;

public interface IUserService
{
    Task<Result<AuthResponse>> RegisterAsync(RegisterRequest request);
    Task<Result<AuthResponse>> LoginAsync(LoginRequest request);
}
