using AutoMapper;
using Microsoft.EntityFrameworkCore;
using GestionNotes.Core.Models;
using GestionNotes.Core.Stores;
using GestionNotes.Infrastructure.Entities;

namespace GestionNotes.Infrastructure.Stores;

public class MatiereStore : IMatiereStore
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public MatiereStore(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<MatiereModel> CreateAsync(MatiereModel model)
    {
        var entity = _mapper.Map<Matiere>(model);
        _db.Matieres.Add(entity);
        await _db.SaveChangesAsync();
        return _mapper.Map<MatiereModel>(entity);
    }

    public async Task<List<MatiereModel>> GetAllAsync()
    {
        var entities = await _db.Matieres.OrderBy(m => m.Code).ToListAsync();
        return _mapper.Map<List<MatiereModel>>(entities);
    }

    public async Task<MatiereModel?> GetByIdAsync(int id)
    {
        var entity = await _db.Matieres.FindAsync(id);
        return entity is null ? null : _mapper.Map<MatiereModel>(entity);
    }

    public async Task<MatiereModel?> UpdateAsync(int id, MatiereModel model)
    {
        var entity = await _db.Matieres.FindAsync(id);
        if (entity is null) return null;

        entity.Code = model.Code;
        entity.Libelle = model.Libelle;
        entity.Coefficient = model.Coefficient;
        await _db.SaveChangesAsync();

        return _mapper.Map<MatiereModel>(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _db.Matieres.FindAsync(id);
        if (entity is null) return false;

        _db.Matieres.Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }
}
