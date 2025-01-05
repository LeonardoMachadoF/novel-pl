using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class DataContext: DbContext
{
    public DataContext(DbContextOptions<DataContext> opts) : base(opts){}
    
    public DbSet<Novel> Novels { get; set; }
    public DbSet<Chapter> Chapters { get; set; }
    public DbSet<Genre> Genres { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Novel>()
            .Property(n => n.OriginalLanguage)
            .HasConversion<int>();
    }
}