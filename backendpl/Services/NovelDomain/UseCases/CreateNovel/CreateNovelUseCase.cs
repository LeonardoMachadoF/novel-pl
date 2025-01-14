using backend.Data.Repository;
using backend.Entities;
using backend.Entities.Dto;
using backendpl.Services.ValidationService;

namespace backend.Services.NovelServices.UseCases.CreateNovel;

public class CreateNovelUseCase : ICreateNovelUseCase
{
    private readonly INovelRepository _novelRepository;
    private readonly IValidationBehavior<CreateNovelDto> _validationBehavior;

    public CreateNovelUseCase(INovelRepository novelRepository, IValidationBehavior<CreateNovelDto> validationBehavior)
    {
        _validationBehavior = validationBehavior;
        _novelRepository = novelRepository;
    }

    public async Task<Novel> Execute(CreateNovelDto createNovelDto)
    {
        _validationBehavior.Validate(createNovelDto);

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