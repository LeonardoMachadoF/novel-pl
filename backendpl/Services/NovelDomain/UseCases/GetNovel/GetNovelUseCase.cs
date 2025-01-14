using backend.Data.Repository;
using backend.Entities;
using backend.Services.NovelDomain.UseCases.GetNovelById;

namespace backend.Services.NovelDomain.UseCases.GetNovel;

public class GetNovelUseCase : IGetNovelUseCase
{
    private readonly INovelRepository _novelRepository;

    public GetNovelUseCase(INovelRepository novelRepository)
    {
        _novelRepository = novelRepository;
    }

    public async Task<Novel?> Execute(Guid id)
    {
        var novel = await _novelRepository.GetNovelById(id);
        if (novel == null)
        {
            throw new Exception("Novel não encontrada4");
        }

        return novel;
    }

    public async Task<Novel?> Execute(string slug)
    {
        var novel = await _novelRepository.GetNovelBySlugWithChapters(slug);
        if (novel == null)
        {
            throw new Exception("Novel não encontrada5");
        }

        return novel;
    }
}