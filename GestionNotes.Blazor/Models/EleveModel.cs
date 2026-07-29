namespace GestionNotes.Blazor.Models;

public class EleveModel
{
    public Guid UserId { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string Prenom { get; set; } = string.Empty;
    public string Matricule { get; set; } = string.Empty;
    public string? Email { get; set; }
}
