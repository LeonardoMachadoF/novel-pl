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
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Novel>()
            .Property(n => n.OriginalLanguage)
            .HasConversion<int>();
        
        modelBuilder.Entity<Novel>()
            .HasMany(n => n.Genres)         
            .WithMany(g => g.Novels)        
            .UsingEntity<Dictionary<string, object>>(
                "NovelGenre",               
                j => j.HasOne<Genre>().WithMany().HasForeignKey("GenreId"),
                j => j.HasOne<Novel>().WithMany().HasForeignKey("NovelId"));
       
        
        modelBuilder.Entity<Chapter>()
            .HasOne(c => c.Novel)
            .WithMany(n => n.Chapters)
            .HasForeignKey(c => c.NovelId);
    }
}