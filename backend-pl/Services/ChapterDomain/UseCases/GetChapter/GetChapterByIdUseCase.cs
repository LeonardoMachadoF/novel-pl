using backend.Data.Repository;
using backend.Entities;

namespace backend.Services.ChapterDomain.UseCases.GetChapter;

public class GetChapterByIdUseCase:IGetChapterByIdUseCase
{
    private readonly IChapterRepository _chapterRepository;
    
    public GetChapterByIdUseCase(IChapterRepository chapterRepository)
    {
        _chapterRepository = chapterRepository;
    }
    
    public async Task<Chapter> Execute(Guid id)
    {
        var chapter = await _chapterRepository.GetChapterById(id);

        if (chapter is null)
        {
            throw new Exception("Chapter not found");
        }
        
        return chapter;
    }
}

