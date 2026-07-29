using GestionNotes.Core.Constants;
using GestionNotes.Core.Models;
using GestionNotes.Core.Results;
using GestionNotes.Core.Services;
using GestionNotes.Core.Stores;

namespace GestionNotes.Application.Services;

public class EleveService : IEleveService
{
    private readonly IEleveStore _eleveStore;
    private readonly IUserStore _userStore;

    public EleveService(IEleveStore eleveStore, IUserStore userStore)
    {
        _eleveStore = eleveStore;
        _userStore = userStore;
    }

    public async Task<Result<EleveModel>> CreateAsync(RegisterRequest request)
    {
        var existing = await _userStore.GetByEmailAsync(request.Email);
        if (existing is not null)
            return Result<EleveModel>.Fail(ErrorMessages.EmailAlreadyExists);

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var userModel = new UserModel
        {
            Email = request.Email,
            RoleId = 2
        };

        var user = await _userStore.CreateAsync(userModel, passwordHash);

        var eleve = new EleveModel
        {
            UserId = user.Id,
            Nom = request.Nom,
            Prenom = request.Prenom,
            Matricule = GenerateMatricule()
        };

        var created = await _eleveStore.CreateAsync(eleve);
        return Result<EleveModel>.Ok(created);
    }

    public async Task<Result<PagedResult<EleveModel>>> GetAllAsync(int page = 1, int pageSize = 20)
    {
        var result = await _eleveStore.GetAllAsync(page, pageSize);
        return Result<PagedResult<EleveModel>>.Ok(result);
    }

    public async Task<Result<EleveModel>> GetByUserIdAsync(Guid userId)
    {
        var eleve = await _eleveStore.GetByUserIdAsync(userId);
        if (eleve is null)
            return Result<EleveModel>.Fail(ErrorMessages.EleveNotFound);

        return Result<EleveModel>.Ok(eleve);
    }

    public async Task<Result<EleveModel>> UpdateAsync(Guid userId, EleveModel model)
    {
        var updated = await _eleveStore.UpdateAsync(userId, model);
        if (updated is null)
            return Result<EleveModel>.Fail(ErrorMessages.EleveNotFound);

        return Result<EleveModel>.Ok(updated);
    }

    public async Task<Result> DeleteAsync(Guid userId)
    {
        var deleted = await _eleveStore.DeleteAsync(userId);
        if (!deleted)
            return Result.Fail(ErrorMessages.EleveNotFound);

        return Result.Ok();
    }

    private static string GenerateMatricule()
    {
        var random = Random.Shared.Next(10000, 99999);
        return $"ETU{DateTime.UtcNow.Year}{random}";
    }
}
