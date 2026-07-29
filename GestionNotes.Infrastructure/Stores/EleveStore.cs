using AutoMapper;
using Microsoft.EntityFrameworkCore;
using GestionNotes.Core.Models;
using GestionNotes.Core.Stores;
using GestionNotes.Infrastructure.Entities;

namespace GestionNotes.Infrastructure.Stores;

public class EleveStore : IEleveStore
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public EleveStore(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<EleveModel> CreateAsync(EleveModel model)
    {
        var entity = _mapper.Map<Eleve>(model);
        _db.Eleves.Add(entity);
        await _db.SaveChangesAsync();
        return _mapper.Map<EleveModel>(entity);
    }

    public async Task<PagedResult<EleveModel>> GetAllAsync(int page, int pageSize)
    {
        var query = _db.Eleves.Include(e => e.User).AsQueryable();
        var total = await query.CountAsync();
        var items = await query
            .OrderBy(e => e.Nom)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<EleveModel>
        {
            Items = _mapper.Map<List<EleveModel>>(items),
            TotalCount = total,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task<EleveModel?> GetByUserIdAsync(Guid userId)
    {
        var entity = await _db.Eleves
            .Include(e => e.User)
            .FirstOrDefaultAsync(e => e.UserId == userId);
        return entity is null ? null : _mapper.Map<EleveModel>(entity);
    }

    public async Task<EleveModel?> UpdateAsync(Guid userId, EleveModel model)
    {
        var entity = await _db.Eleves.FindAsync(userId);
        if (entity is null) return null;

        entity.Nom = model.Nom;
        entity.Prenom = model.Prenom;
        entity.Matricule = model.Matricule;
        await _db.SaveChangesAsync();

        return _mapper.Map<EleveModel>(entity);
    }

    public async Task<bool> DeleteAsync(Guid userId)
    {
        var entity = await _db.Eleves.FindAsync(userId);
        if (entity is null) return false;

        _db.Eleves.Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }
}
