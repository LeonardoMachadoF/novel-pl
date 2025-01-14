using backend.Data.Repository;

namespace backend.Services.NovelServices.UseCases.DeleteNovel;

public class DeleteNovelUseCase : IDeleteNovelUseCase
{
    private readonly INovelRepository _novelRepository;

    public DeleteNovelUseCase(INovelRepository novelRepository)
    {
        _novelRepository = novelRepository;
    }

    public async Task Execute(Guid id)
    {
        var novel = await _novelRepository.GetNovelById(id);
        if (novel == null)
            throw new Exception("Novel não encontrada");

        await _novelRepository.DeleteNovel(novel);
    }
}