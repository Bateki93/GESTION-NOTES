namespace GestionNotes.Infrastructure.Entities;

public class Eleve
{
    public Guid UserId { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string Prenom { get; set; } = string.Empty;
    public string Matricule { get; set; } = string.Empty;

    public User User { get; set; } = null!;
    public ICollection<Note> Notes { get; set; } = [];
}
