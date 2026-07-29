namespace GestionNotes.Blazor.Models;

public class AuthResponse
{
    public string Token { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public int RoleId { get; set; }
}
