using backend.Data.Repository;
using backend.Entities;
using backend.Validators;
using backendpl.Services.ValidationService;

namespace backend.Services.NovelServices.UseCases.GetNovels;

public class GetNovelsUseCase : IGetNovelsUseCase
{
    private readonly INovelRepository _novelRepository;
    private readonly IValidationBehavior<IPagination> _validationBehavior;

    public GetNovelsUseCase(INovelRepository novelRepository, IValidationBehavior<IPagination> validationBehavior)
    {
        _novelRepository = novelRepository;
        _validationBehavior = validationBehavior;
    }

    public async Task<List<Novel>> Execute(int take, int skip)
    {
        _validationBehavior.Validate(new Pagination(take, skip));
        var novels = await _novelRepository.GetNovels(take, skip);
        return novels;
    }
}