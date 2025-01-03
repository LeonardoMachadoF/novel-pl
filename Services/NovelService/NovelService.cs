using backend.Data.Repository;
using backend.Entities;
using FluentValidation;

namespace backend.Services.NovelService;

public class NovelService:INovelService
{
    private readonly INovelRepository _novelRepository;
    private readonly IValidator<NovelDTO> _validatorNovelDto;

    public NovelService(INovelRepository novelRepository,IValidator<NovelDTO> validator)
    {
        _novelRepository = novelRepository;
        _validatorNovelDto = validator;
    }
    
    public async Task<Novel> CreateNovel(NovelDTO novelDto)
    {
        var  validationResult =  _validatorNovelDto.Validate(novelDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        var newNovel = new Novel
        {
            Title = novelDto.Title,
            Description = novelDto.Description
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

