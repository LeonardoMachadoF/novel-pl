using backend.Entities;
using backend.Entities.Dto;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Repository;

public class NovelRepository : INovelRepository
{
    private readonly DataContext _context;

    public NovelRepository(DataContext context)
    {
        _context = context;
    }

    public async Task Add(Novel novel)
    {
        var newNovel = _context.Novels.Add(novel);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Novel>> GetNovels(int take, int skip)
    {
        var novels = await _context.Novels.Skip(skip).Take(take).ToListAsync();
        return novels;
    }

    public async Task<Novel?> GetNovelById(Guid novelId)
    {
        var novel = await _context.Novels.Include(x => x.Chapters).Include(x => x.Genres)
            .FirstOrDefaultAsync(novel => novel.Id == novelId);
        return novel;
    }

    public async Task<Novel> GetNovelBySlug(string slug)
    {
        var novel = await _context.Novels
            .FirstOrDefaultAsync(x => x.Slug == slug);
        
        return novel;
    }

    public async Task<Novel?> GetNovelBySlugWithChapters(string slug)
    {
        var novel = await _context.Novels
            .Include(x => x.Chapters)
            .Include(x => x.Genres)
            .FirstOrDefaultAsync(x => x.Slug == slug);
        
        return novel;
    } 

    public async Task UpdateNovel(Novel existentNovel)
    {
        _context.Entry(existentNovel).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public Task DeleteNovel(Novel novel)
    {
        _context.Novels.Remove(novel);
        return _context.SaveChangesAsync();
    }
}