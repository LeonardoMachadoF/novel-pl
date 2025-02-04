using backend.Entities;

namespace backend.Services.ChapterDomain.UseCases.GetChapter;

public interface IGetChapterByIdUseCase
{
    Task<Chapter?> Execute(Guid id);
}