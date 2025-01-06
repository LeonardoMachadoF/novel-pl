using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Repository;

public class ChapterRepository : IChapterRepository
{
    private readonly DataContext _context;
    public ChapterRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task Add(Chapter chapter)
    {
        _context.Chapters.Add(chapter);
        await _context.SaveChangesAsync();
    }

    public Task<List<Chapter>> GetChapters(int take, int skip)
    {
        throw new NotImplementedException();
    }

    public async Task<Chapter?> GetChapterById(Guid id)
    {
        var chapter = await _context.Chapters.FirstOrDefaultAsync(x=>x.Id == id);
        return chapter;;
    }

    public async Task UpdateChapter(Chapter chapter)
    {
        _context.Entry(chapter).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteChapter(Chapter novel)
    {
        _context.Chapters.Remove(novel);
        await _context.SaveChangesAsync();
    }
    
}