using AutoMapper;
using Microsoft.EntityFrameworkCore;
using GestionNotes.Core.Models;
using GestionNotes.Core.Stores;
using GestionNotes.Infrastructure.Entities;

namespace GestionNotes.Infrastructure.Stores;

public class NoteStore : INoteStore
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public NoteStore(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<NoteModel> CreateAsync(NoteModel model)
    {
        var entity = _mapper.Map<Note>(model);
        entity.CreatedAt = DateTime.UtcNow;
        _db.Notes.Add(entity);
        await _db.SaveChangesAsync();
        return await GetByIdAsync(entity.Id)
            ?? throw new InvalidOperationException("Note not found after creation");
    }

    public async Task<PagedResult<NoteModel>> GetAllAsync(int page, int pageSize)
    {
        var query = _db.Notes
            .OrderByDescending(n => n.CreatedAt);

        var total = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(n => new NoteModel
            {
                Id = n.Id,
                EleveId = n.EleveId,
                EleveNom = n.Eleve.Nom,
                ElevePrenom = n.Eleve.Prenom,
                EleveMatricule = n.Eleve.Matricule,
                MatiereId = n.MatiereId,
                MatiereLibelle = n.Matiere.Libelle,
                Valeur = n.Valeur,
                Semestre = n.Semestre,
                Annee = n.Annee,
                CreatedAt = n.CreatedAt
            })
            .ToListAsync();

        return new PagedResult<NoteModel>
        {
            Items = items,
            TotalCount = total,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task<List<NoteModel>> GetByEleveIdAsync(Guid eleveId)
    {
        return await _db.Notes
            .Where(n => n.EleveId == eleveId)
            .OrderByDescending(n => n.CreatedAt)
            .Select(n => new NoteModel
            {
                Id = n.Id,
                EleveId = n.EleveId,
                EleveNom = n.Eleve.Nom,
                ElevePrenom = n.Eleve.Prenom,
                EleveMatricule = n.Eleve.Matricule,
                MatiereId = n.MatiereId,
                MatiereLibelle = n.Matiere.Libelle,
                Valeur = n.Valeur,
                Semestre = n.Semestre,
                Annee = n.Annee,
                CreatedAt = n.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<NoteModel?> GetByIdAsync(int id)
    {
        return await _db.Notes
            .Where(n => n.Id == id)
            .Select(n => new NoteModel
            {
                Id = n.Id,
                EleveId = n.EleveId,
                EleveNom = n.Eleve.Nom,
                ElevePrenom = n.Eleve.Prenom,
                EleveMatricule = n.Eleve.Matricule,
                MatiereId = n.MatiereId,
                MatiereLibelle = n.Matiere.Libelle,
                Valeur = n.Valeur,
                Semestre = n.Semestre,
                Annee = n.Annee,
                CreatedAt = n.CreatedAt
            })
            .FirstOrDefaultAsync();
    }

    public async Task<NoteModel?> UpdateAsync(int id, NoteModel model)
    {
        var entity = await _db.Notes.FindAsync(id);
        if (entity is null) return null;

        entity.EleveId = model.EleveId;
        entity.MatiereId = model.MatiereId;
        entity.Valeur = model.Valeur;
        entity.Semestre = model.Semestre;
        entity.Annee = model.Annee;
        await _db.SaveChangesAsync();

        return await GetByIdAsync(id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _db.Notes.FindAsync(id);
        if (entity is null) return false;

        _db.Notes.Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }
}
