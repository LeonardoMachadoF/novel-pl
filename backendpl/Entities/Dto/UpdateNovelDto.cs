using backend.Data.Enums;

namespace backend.Entities.Dto;

public class UpdateNovelDto
{
    public string? Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;

    public NovelOriginalLanguage? OriginalLanguage { get; set; }

    public string? ImageUrl { get; set; } = string.Empty;
}