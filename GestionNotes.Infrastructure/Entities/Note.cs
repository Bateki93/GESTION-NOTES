namespace GestionNotes.Infrastructure.Entities;

public class Note
{
    public int Id { get; set; }
    public Guid EleveId { get; set; }
    public int MatiereId { get; set; }
    public decimal Valeur { get; set; }
    public int Semestre { get; set; }
    public string Annee { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public Eleve Eleve { get; set; } = null!;
    public Matiere Matiere { get; set; } = null!;
}
