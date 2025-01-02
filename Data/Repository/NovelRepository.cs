using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Repository;

public class NovelRepository:INovelRepository
{
    private readonly DataContext _context;
    public NovelRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task Add(Novel novel)
    {
        _context.Novels.Add(novel);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Novel>> FindNovels()
    {
        var novels = await _context.Novels.ToListAsync();
        return novels;
    }
}