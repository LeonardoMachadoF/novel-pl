using backend.Data.Repository;
using backend.Entities;

namespace backend.Services.NovelService;

public class NovelService:INovelService
{
    private readonly INovelRepository _novelRepository;

    public NovelService(INovelRepository novelRepository)
    {
        _novelRepository = novelRepository;
    }
    
    public async Task<Novel> CreateNovel(NovelDTO novel)
    {
        var newNovel = new Novel
        {
            Title = novel.Title,
            Description = novel.Description
        };
        await _novelRepository.Add(newNovel);
        return newNovel;
    }

    public async Task<List<Novel>> GetNovels()
    {
        var novels = await _novelRepository.FindNovels();
        return novels;
    }
}

