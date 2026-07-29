using AutoMapper;
using Microsoft.EntityFrameworkCore;
using GestionNotes.Core.Models;
using GestionNotes.Core.Stores;
using GestionNotes.Infrastructure.Entities;

namespace GestionNotes.Infrastructure.Stores;

public class UserStore : IUserStore
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public UserStore(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<UserModel?> GetByIdAsync(Guid id)
    {
        var user = await _db.Users.FindAsync(id);
        return user is null ? null : _mapper.Map<UserModel>(user);
    }

    public async Task<UserModel?> GetByEmailAsync(string email)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        return user is null ? null : _mapper.Map<UserModel>(user);
    }

    public async Task<UserModel> CreateAsync(UserModel model, string passwordHash)
    {
        var entity = new User
        {
            Id = Guid.NewGuid(),
            Email = model.Email,
            PasswordHash = passwordHash,
            RoleId = model.RoleId,
            CreatedAt = DateTime.UtcNow
        };

        _db.Users.Add(entity);
        await _db.SaveChangesAsync();

        return _mapper.Map<UserModel>(entity);
    }

    public async Task<string?> GetPasswordHashAsync(Guid userId)
    {
        var user = await _db.Users.FindAsync(userId);
        return user?.PasswordHash;
    }
}
