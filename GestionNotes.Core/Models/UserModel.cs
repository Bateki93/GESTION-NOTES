namespace GestionNotes.Core.Models;

public class UserModel
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public int RoleId { get; set; }
}
