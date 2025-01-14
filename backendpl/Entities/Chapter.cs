using System.Globalization;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace backend.Entities;

public class Chapter
{
    public Chapter(string title, int number, int volume, string content)
    {
        Id = Guid.NewGuid();
        Title = title;
        Number = number;
        Volume = volume;
        Content = content;

        ReleaseDate = DateTime.Now;
    }

    public Guid Id { get; set; }
    public int Number { get; set; }
    public int Volume { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime ReleaseDate { get; set; }
    [JsonIgnore] public Novel Novel { get; set; }
    public Guid NovelId { get; set; }
}