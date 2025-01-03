using backend.Entities;
using backend.Entities.Dto;
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

    public async Task<List<Novel>> GetNovels(int take, int skip)
    {
        var novels = await _context.Novels.Skip(skip).Take(take).ToListAsync();
        return novels;
    }

    public async Task<Novel?> GetNovelById(Guid novelId)
    {
        var novel = await _context.Novels.FirstOrDefaultAsync(novel=>novel.Id==novelId);
        return novel;
    }

    public async Task<Novel> UpdateNovel(Novel existentNovel, UpdateNovelDto updateNovelDto)
    {
        var properties = updateNovelDto.GetType().GetProperties();
    
        foreach (var property in properties)
        {
            var value = property.GetValue(updateNovelDto);
            
            if (value != null && !(value is string str && str == ""))
            {
                _context.Entry(existentNovel).Property(property.Name).CurrentValue = value;
            }
        }
        
        await _context.SaveChangesAsync();
        
        return existentNovel;
    }

    public Task DeleteNovel(Novel novel)
    {
        _context.Novels.Remove(novel);
        return _context.SaveChangesAsync();
    }
}