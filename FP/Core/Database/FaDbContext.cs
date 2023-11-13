using FP.Core.Database.Models;
using FP.Core.Global;
using Microsoft.EntityFrameworkCore;

namespace FP.Core.Database;

public class FpDbContext : DbContext
{
    public DbSet<FpUser> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(ApplicationData.ConnectionString);
    }
}
