using backend.Entities;
using backend.Entities.Dto;

namespace backend.Services.NovelService;

public interface INovelService
{ 
    Task<Novel> CreateNovel(CreateNovelDto createNovelDto);
    Task<List<Novel>> GetNovels(int take, int skip);
    Task<Novel?> GetNovelById(Guid id);
    Task<Novel> UpdateNovel(Guid id,UpdateNovelDto updateNovelDto);
    
    Task DeleteNovel(Guid id);
}