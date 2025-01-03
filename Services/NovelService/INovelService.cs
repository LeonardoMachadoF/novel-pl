using backend.Entities;

namespace backend.Services.NovelService;

public interface INovelService
{ 
    Task<Novel> CreateNovel(NovelDTO novelDto);
    Task<List<Novel>> GetNovels();
}