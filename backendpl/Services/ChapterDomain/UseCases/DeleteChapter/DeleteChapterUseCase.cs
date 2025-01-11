using System.Linq.Expressions;
using backend.Data.Repository;

namespace backend.Services.ChapterDomain.UseCases.DeleteChapter;

public class DeleteChapterUseCase: IDeleteChapterUseCase
{
    private readonly IChapterRepository _chapterRepository;
    
    public DeleteChapterUseCase(IChapterRepository chapterRepository)
    {
        _chapterRepository = chapterRepository;
    }
    public async Task Execute(Guid id)
    {
        var chapter = await _chapterRepository.GetChapterById(id);
        if (chapter is null) throw new KeyNotFoundException("Capitulo não encontrada!");
        await _chapterRepository.DeleteChapter(chapter);
    }
}