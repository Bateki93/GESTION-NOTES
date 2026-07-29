using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using GestionNotes.Core.Constants;
using GestionNotes.Core.Models;
using GestionNotes.Core.Results;
using GestionNotes.Core.Services;
using GestionNotes.Core.Stores;

namespace GestionNotes.Application.Services;

public class UserService : IUserService
{
    private readonly IUserStore _userStore;
    private readonly IEleveStore _eleveStore;

    public UserService(IUserStore userStore, IEleveStore eleveStore)
    {
        _userStore = userStore;
        _eleveStore = eleveStore;
    }

    public async Task<Result<AuthResponse>> RegisterAsync(RegisterRequest request)
    {
        var existing = await _userStore.GetByEmailAsync(request.Email);
        if (existing is not null)
            return Result<AuthResponse>.Fail(ErrorMessages.EmailAlreadyExists);

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var userModel = new UserModel
        {
            Email = request.Email,
            RoleId = request.RoleId
        };

        var user = await _userStore.CreateAsync(userModel, passwordHash);

        if (request.RoleId == 2)
        {
            var eleve = new EleveModel
            {
                UserId = user.Id,
                Nom = request.Nom,
                Prenom = request.Prenom,
                Matricule = GenerateMatricule()
            };
            await _eleveStore.CreateAsync(eleve);
        }

        var token = GenerateJwtToken(user);

        return Result<AuthResponse>.Ok(new AuthResponse
        {
            Token = token,
            UserId = user.Id,
            Email = user.Email,
            RoleId = user.RoleId
        });
    }

    public async Task<Result<AuthResponse>> LoginAsync(LoginRequest request)
    {
        var user = await _userStore.GetByEmailAsync(request.Email);
        if (user is null)
            return Result<AuthResponse>.Fail(ErrorMessages.InvalidCredentials);

        var passwordHash = await _userStore.GetPasswordHashAsync(user.Id);
        if (passwordHash is null || !BCrypt.Net.BCrypt.Verify(request.Password, passwordHash))
            return Result<AuthResponse>.Fail(ErrorMessages.InvalidCredentials);

        var token = GenerateJwtToken(user);

        return Result<AuthResponse>.Ok(new AuthResponse
        {
            Token = token,
            UserId = user.Id,
            Email = user.Email,
            RoleId = user.RoleId
        });
    }

    private static string GenerateJwtToken(UserModel user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSuperSecretKeyForJwtTokenGeneration2024!"));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.RoleId.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: "GestionNotes",
            audience: "GestionNotesUsers",
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static string GenerateMatricule()
    {
        var random = Random.Shared.Next(10000, 99999);
        return $"ETU{DateTime.UtcNow.Year}{random}";
    }
}
