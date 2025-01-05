using backend.Data.Repository;
using backend.Entities;
using backend.Entities.Dto;
using backend.Services.ErrorService;
using backend.Services.ValidationService;
using backend.Validators;
using FluentValidation;

namespace backend.Services.NovelService;

public class NovelService:INovelService
{
    private readonly INovelRepository _novelRepository;
    private readonly INovelValidationService _validationService;

    public NovelService(INovelRepository novelRepository, INovelValidationService validationService)
    {
        _novelRepository = novelRepository;
        _validationService = validationService;
    }
    
    public async Task<Novel> CreateNovel(CreateNovelDto createNovelDto)
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

    public async Task<List<Novel>> GetNovels(int take, int skip)
    {
        _validationService.ValidadePagination(new Pagination(take, skip));
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

    public async Task<Novel> UpdateNovel(Guid id, UpdateNovelDto updateNovelDto)
    {
        _validationService.ValidadeUpdate(updateNovelDto);
        
        var existentNovel = await _novelRepository.GetNovelById(id);
        if (existentNovel == null)
            throw new Exception("Novel não encontrada");
        
        var properties = updateNovelDto.GetType().GetProperties();

        foreach (var property in properties)
        {
            var valueFromDto = property.GetValue(updateNovelDto);
            var targetProperty = existentNovel.GetType().GetProperty(property.Name);
            
            if (!StringIsNotNullNorEmpty(valueFromDto) || targetProperty == null) continue;
            
            targetProperty.SetValue(existentNovel, valueFromDto);
        }
        
        return await _novelRepository.UpdateNovel(existentNovel);
    }

    public async Task DeleteNovel(Guid id)
    {
        var novel = await _novelRepository.GetNovelById(id);
        if (novel == null)
            throw new Exception("Novel não encontrada");
        
        await _novelRepository.DeleteNovel(novel);
    }
    
    
    private bool StringIsNotNullNorEmpty(object? value)
    {
        return (value != null && !(value is string str && str == ""));
    }
}

