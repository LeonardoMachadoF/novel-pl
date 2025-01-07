using backend.Entities;
using backend.Entities.Dto;
using Microsoft.AspNetCore.Http.HttpResults;

namespace backend.Data.Repository;

public interface INovelRepository
{
    Task Add(Novel novel);
    Task<List<Novel>> GetNovels(int take, int skip);
    
    Task<Novel?> GetNovelById(Guid id);
    
    Task UpdateNovel(Novel novel);
    
    Task DeleteNovel(Novel novel);
    
    Task<Novel> GetNovelBySlug(string slug);
    Task<Novel?> GetNovelBySlugWithChapters(string slug);
}
