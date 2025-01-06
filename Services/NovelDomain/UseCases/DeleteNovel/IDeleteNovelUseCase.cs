namespace backend.Services.NovelServices.UseCases.DeleteNovel;

public interface IDeleteNovelUseCase
{
    Task Execute(Guid id);
}