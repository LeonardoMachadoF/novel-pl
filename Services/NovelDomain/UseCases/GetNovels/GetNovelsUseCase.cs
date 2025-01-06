using backend.Data.Repository;
using backend.Entities;
using backend.Services.ValidationService;
using backend.Validators;

namespace backend.Services.NovelServices.UseCases.GetNovels;

public class GetNovelsUseCase: IGetNovelsUseCase
{
    private readonly INovelRepository _novelRepository;
    private readonly INovelValidationService _validationService;

    public GetNovelsUseCase(INovelRepository novelRepository, INovelValidationService validationService)
    {
        _novelRepository = novelRepository;
        _validationService = validationService;
    }
    
    public async Task<List<Novel>> Execute(int take, int skip)
    {
        _validationService.ValidadePagination(new Pagination(take, skip));
        var novels = await _novelRepository.GetNovels(take, skip);
        return novels;
    }
}