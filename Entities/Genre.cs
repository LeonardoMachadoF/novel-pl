namespace backend.Entities;

public class Genre
{
    public Genre(string name, string? description = null)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        CreatedDate = DateTime.UtcNow;
    }
    
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public int ViewCount { get; set; } = 0;
    public int LikeCount { get; set; } = 0;
    public DateTime CreatedDate { get; set; }
    
    public List<Guid> NovelIds { get; set; }
    public List<Novel> Novels { get; set; }
}