using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using GestionNotes.Infrastructure.Entities;

namespace GestionNotes.Infrastructure;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Database=GestionNotesDb;Username=justin;Password=123456");

        return new AppDbContext(optionsBuilder.Options);
    }
}
