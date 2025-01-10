using backend.Entities;

namespace backend.Services.NovelServices.UseCases.GetNovels;

public interface IGetNovelsUseCase
{
    Task<List<Novel>> Execute(int take, int skip);
}