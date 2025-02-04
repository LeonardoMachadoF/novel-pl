using backend.Data.Repository;
using backend.Entities;

namespace backend.Services.ChapterDomain.UseCases.GetChapter;

public class GetChapterByIdUseCase(IChapterRepository chapterRepository) : IGetChapterByIdUseCase
{
    public async Task<Chapter?> Execute(Guid id)
    {
        var chapter = await chapterRepository.GetChapterById(id);

        if (chapter is null)
        {
            throw new Exception("Chapter not found");
        }

        return chapter;
    }
}