namespace GestionNotes.Infrastructure.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public int RoleId { get; set; }
    public DateTime CreatedAt { get; set; }

    public RoleType RoleType { get; set; } = null!;
    public Eleve? Eleve { get; set; }
}
