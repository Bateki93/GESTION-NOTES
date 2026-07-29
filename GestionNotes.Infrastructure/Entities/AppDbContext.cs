using Microsoft.EntityFrameworkCore;

namespace GestionNotes.Infrastructure.Entities;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<RoleType> RoleTypes => Set<RoleType>();
    public DbSet<Eleve> Eleves => Set<Eleve>();
    public DbSet<Matiere> Matieres => Set<Matiere>();
    public DbSet<Note> Notes => Set<Note>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(e =>
        {
            e.HasKey(u => u.Id);
            e.HasIndex(u => u.Email).IsUnique();
            e.Property(u => u.Email).HasMaxLength(255);
            e.Property(u => u.PasswordHash).HasMaxLength(255);
            e.HasOne(u => u.RoleType)
             .WithMany()
             .HasForeignKey(u => u.RoleId);
        });

        modelBuilder.Entity<RoleType>(e =>
        {
            e.HasKey(r => r.Id);
            e.Property(r => r.Label).HasMaxLength(50);
            e.HasData(
                new RoleType { Id = 1, Label = "Admin" },
                new RoleType { Id = 2, Label = "Eleve" }
            );
        });

        modelBuilder.Entity<Eleve>(e =>
        {
            e.HasKey(el => el.UserId);
            e.Property(el => el.Nom).HasMaxLength(100);
            e.Property(el => el.Prenom).HasMaxLength(100);
            e.Property(el => el.Matricule).HasMaxLength(20);
            e.HasIndex(el => el.Matricule).IsUnique();
            e.HasOne(el => el.User)
             .WithOne(u => u.Eleve)
             .HasForeignKey<Eleve>(el => el.UserId);
        });

        modelBuilder.Entity<Matiere>(e =>
        {
            e.HasKey(m => m.Id);
            e.Property(m => m.Code).HasMaxLength(10);
            e.Property(m => m.Libelle).HasMaxLength(200);
            e.HasIndex(m => m.Code).IsUnique();
        });

        modelBuilder.Entity<Note>(e =>
        {
            e.HasKey(n => n.Id);
            e.Property(n => n.Valeur).HasColumnType("decimal(4,2)");
            e.Property(n => n.Annee).HasMaxLength(9);
            e.HasOne(n => n.Eleve)
             .WithMany(el => el.Notes)
             .HasForeignKey(n => n.EleveId);
            e.HasOne(n => n.Matiere)
             .WithMany()
             .HasForeignKey(n => n.MatiereId);
            e.HasIndex(n => new { n.EleveId, n.MatiereId, n.Semestre, n.Annee });
        });
    }
}
