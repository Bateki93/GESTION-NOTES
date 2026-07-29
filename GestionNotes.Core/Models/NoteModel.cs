namespace GestionNotes.Core.Models;

public class NoteModel
{
    public int Id { get; set; }
    public Guid EleveId { get; set; }
    public string? EleveNom { get; set; }
    public string? ElevePrenom { get; set; }
    public string? EleveMatricule { get; set; }
    public int MatiereId { get; set; }
    public string? MatiereLibelle { get; set; }
    public decimal Valeur { get; set; }
    public int Semestre { get; set; }
    public string Annee { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
