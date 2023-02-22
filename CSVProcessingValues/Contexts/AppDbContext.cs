using CSVProcessingValues.Models;
using Microsoft.EntityFrameworkCore;

namespace CSVProcessingValues.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        // Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    public AppDbContext()
    {
        
    }
    
    public virtual DbSet<Value>? Values { get; set; }
    public virtual DbSet<Result>? Results { get; set; }
}