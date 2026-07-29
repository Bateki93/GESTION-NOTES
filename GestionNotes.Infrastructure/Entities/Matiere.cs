namespace GestionNotes.Infrastructure.Entities;

public class Matiere
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Libelle { get; set; } = string.Empty;
    public int Coefficient { get; set; }
}
