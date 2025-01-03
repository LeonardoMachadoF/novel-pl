using backend.Data.Repository;
using backend.Entities;
using backend.Entities.Dto;
using backend.Services.ErrorService;
using FluentValidation;

namespace backend.Services.NovelService;

public class NovelService:INovelService
{
    private readonly INovelRepository _novelRepository;
    private readonly IValidator<NovelDto> _validatorCreateNovelDto;
    private readonly IErrorService _errorService;
    private readonly IValidator<NovelUpdateDto> _validatorUpdateNovelDto;

    public NovelService(INovelRepository novelRepository,IValidator<NovelDto> validatorCreateNovel, IErrorService errorService, IValidator<NovelUpdateDto> validatorUpdateNovelDto)
    {
        _novelRepository = novelRepository;
        _validatorCreateNovelDto = validatorCreateNovel;
        _validatorUpdateNovelDto = validatorUpdateNovelDto;
        _errorService = errorService;
    }
    
    public async Task<Novel> CreateNovel(NovelDto novelDto)
    {
        var  validationResult =  _validatorCreateNovelDto.Validate(novelDto);
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

    public async Task<List<Novel>> GetNovels(int take, int skip)
    {
        if (take <= 0 || skip < 0)
        {
            throw new ArgumentException("Parâmetros 'take' e 'skip' devem ser positivos.");
        }
        var novels = await _novelRepository.GetNovels(take, skip);
        return novels;
    }

    public async Task<Novel?> GetNovelById(Guid id)
    {
        var novel = await _novelRepository.GetNovelById(id);
        if (novel == null)
        {
            throw new Exception("Novel não encontrada");
        }
        return novel;
    }

    public async Task<Novel> UpdateNovel(Guid id, NovelUpdateDto novelUpdateDto)
    {
        var  validationResult =  _validatorUpdateNovelDto.Validate(novelUpdateDto);
        if (!validationResult.IsValid)
        {
            var errors = _errorService.SanitazeError(validationResult.Errors);
            throw new ErrorCustomException(errors);
        }
        
        var existentNovel = await _novelRepository.GetNovelById(id);
        if (existentNovel == null)
        {
            throw new Exception("Novel não encontrada");
        }

        return await _novelRepository.UpdateNovel(existentNovel, novelUpdateDto);
    }

    public async Task DeleteNovel(Guid id)
    {
        var novel = await _novelRepository.GetNovelById(id);
        if (novel == null)
            throw new Exception("Novel não encontrada");
        
        await _novelRepository.DeleteNovel(novel);
    }
}

