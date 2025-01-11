using backend.Data.Repository;
using backend.Entities;
using backend.Entities.Dto;
using backend.Services.ValidationService;

namespace backend.Services.NovelServices.UseCases.CreateNovel;

public class CreateNovelUseCase: ICreateNovelUseCase
{
    private readonly INovelRepository _novelRepository;
    private readonly INovelValidationService _validationService;

    public CreateNovelUseCase(INovelRepository novelRepository, INovelValidationService validationService)
    {
        _novelRepository = novelRepository;
        _validationService = validationService;
    }
    public async Task<Novel> Execute(CreateNovelDto createNovelDto)
    {
        _validationService.ValidateCreate(createNovelDto);
        var newNovel = new Novel(
            createNovelDto.Title,
            createNovelDto.Description,
            createNovelDto.OriginalLanguage,
            createNovelDto.ImageUrl
        );
        await _novelRepository.Add(newNovel);
        
        return newNovel;
    }
}