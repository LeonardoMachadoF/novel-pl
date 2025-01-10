using backend.Entities;

namespace backend.Services.NovelDomain.UseCases.GetNovelById;

public interface IGetNovelUseCase
{
    Task<Novel?> Execute(Guid id);
    Task<Novel?> Execute(string id);
}