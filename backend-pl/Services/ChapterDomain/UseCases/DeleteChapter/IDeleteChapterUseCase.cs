namespace backend.Services.ChapterDomain.UseCases.DeleteChapter;

public interface IDeleteChapterUseCase
{
    Task Execute(Guid id);
}