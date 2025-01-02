using backend.Entities;

namespace backend.Services.NovelService;

public interface INovelService
{ 
    Task<Novel> CreateNovel(NovelDTO novel);
    Task<List<Novel>> GetNovels();
}