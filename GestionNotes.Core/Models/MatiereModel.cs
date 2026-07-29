namespace GestionNotes.Core.Models;

public class MatiereModel
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Libelle { get; set; } = string.Empty;
    public int Coefficient { get; set; }
}
