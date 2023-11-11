using Microsoft.EntityFrameworkCore;

namespace FP.Core.Database.Models;

public class FpDbContext : DbContext
{
    public DbSet<FpUser> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("User Id=postgres;Password=123;Host=localhost;Port=5432;Database=FP;Pooling=true;");
    }
}
