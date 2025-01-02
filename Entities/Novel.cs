namespace backend.Entities;

public class Novel
{
    public Novel()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; init; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}