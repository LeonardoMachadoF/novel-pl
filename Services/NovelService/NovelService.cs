using backend.Data.Repository;
using backend.Entities;
using backend.Services.ErrorService;
using FluentValidation;

namespace backend.Services.NovelService;

public class NovelService:INovelService
{
    private readonly INovelRepository _novelRepository;
    private readonly IValidator<NovelDTO> _validatorNovelDto;
    private readonly IErrorService _errorService;

    public NovelService(INovelRepository novelRepository,IValidator<NovelDTO> validator, IErrorService errorService)
    {
        _novelRepository = novelRepository;
        _validatorNovelDto = validator;
        _errorService = errorService;
    }
    
    public async Task<Novel> CreateNovel(NovelDTO novelDto)
    {
        var  validationResult =  _validatorNovelDto.Validate(novelDto);
        if (!validationResult.IsValid)
        {
            var errors = _errorService.SanitazeError(validationResult.Errors);
            throw new ErrorCustomException(errors);
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

