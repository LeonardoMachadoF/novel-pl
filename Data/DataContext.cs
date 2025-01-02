using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class DataContext: DbContext
{
    public DataContext(DbContextOptions<DataContext> opts) : base(opts){}
    
    public DbSet<Novel> Novels { get; set; }
}