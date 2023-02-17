using CSVProcessingValues.Models;
using Microsoft.EntityFrameworkCore;

namespace CSVProcessingValues.Contexts;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        // Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    public DbSet<Value>? Values { get; set; }
    public DbSet<Result>? Results { get; set; }
}