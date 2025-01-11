using backend.Entities;

namespace backend.Data.Repository;

public interface IChapterRepository
{
    Task Add(Chapter novel);

    Task<Chapter?> GetChapterById(Guid id);

    Task UpdateChapter(Chapter chapter);

    Task DeleteChapter(Chapter chapter);
}