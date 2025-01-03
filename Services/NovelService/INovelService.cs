using backend.Entities;
using backend.Entities.Dto;

namespace backend.Services.NovelService;

public interface INovelService
{ 
    Task<Novel> CreateNovel(NovelDto novelDto);
    Task<List<Novel>> GetNovels(int take, int skip);
    Task<Novel?> GetNovelById(Guid id);
    Task<Novel> UpdateNovel(Guid id,NovelUpdateDto novelDto);
    
    Task DeleteNovel(Guid id);
}