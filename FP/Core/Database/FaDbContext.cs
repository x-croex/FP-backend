﻿using FP.Core.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace FP.Core.Database;

public class FpDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Pack> Packs { get; set; }
    public DbSet<PackType> PackTypes { get; set; }
    public DbSet<Wallet> Wallets { get; set; }

    public FpDbContext(DbContextOptions<FpDbContext> dbContextOptions) : base(dbContextOptions) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
}
