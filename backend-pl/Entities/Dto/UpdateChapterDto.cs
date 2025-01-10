namespace backend.Entities.Dto;

public class UpdateChapterDto
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int? Number { get; set; }
    public int? Volume { get; set; }
}