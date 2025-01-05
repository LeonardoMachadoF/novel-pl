using backend.Data.Enums;

namespace backend.Entities;

public class Novel
{
    public Novel(string title, string description, NovelOriginalLanguage originalLanguage, string? imageUrl)
    {
        Id = Guid.NewGuid(); 
        Title = title;
        Description = description; 
        OriginalLanguage = originalLanguage;
        ImageUrl = imageUrl;
        
        LastUpdate = DateTime.UtcNow; 
        Status = NovelStatus.Releasing;
        Genres = new List<Genre>(); 
        Chapters = new List<Chapter>();
        Author = new List<string>();
    }

    public Guid Id { get; init; }
    public string Title { get; set; }
    public string Description { get; set; }
    public NovelOriginalLanguage OriginalLanguage { get; set; }
    public string ImageUrl { get; set; } = "default.jpg";
    
    
    public string CoverImageUrl { get; set; } = "default.jpg"; 
    public NovelStatus Status { get; set; }
    public double Rating { get; set; } 
    public int ViewCount { get; set; } 
    public int LikeCount { get; set; } 
    public List<string> Author { get; set; }
    public DateTime LastUpdate { get; set; }
    
    public List<Genre> Genres { get; set; }
    public List<Chapter> Chapters { get; set; }
}