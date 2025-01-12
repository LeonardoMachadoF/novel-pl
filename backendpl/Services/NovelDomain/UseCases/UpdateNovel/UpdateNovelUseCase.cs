using backend.Data.Repository;
using backend.Entities;
using backend.Entities.Dto;
using backend.Utils;
using backendpl.Services.ValidationService;

namespace backend.Services.NovelServices.UseCases.UpdateNovel;

public class UpdateNovelUseCase: IUpdateNovelUseCase
{
    private readonly INovelRepository _novelRepository;
    private readonly IValidationBehavior<UpdateNovelDto> _validationBehavior;

    public UpdateNovelUseCase(INovelRepository novelRepository, IValidationBehavior<UpdateNovelDto> validationBehavior)
    {
        _novelRepository = novelRepository;
        _validationBehavior = validationBehavior;
    }
    
    public async Task<Novel> Execute(string slug, UpdateNovelDto updateNovelDto)
    {
        _validationBehavior.Validate(updateNovelDto);
        
        var existentNovel = await _novelRepository.GetNovelBySlug(slug);
        if (existentNovel == null)
            throw new Exception("Novel não encontrada");
        
        var novelDtoProperties = updateNovelDto.GetType().GetProperties();

        foreach (var property in novelDtoProperties)
        {
            var valueFromDto = property.GetValue(updateNovelDto);
            var targetProperty = existentNovel.GetType().GetProperty(property.Name);
            
            if (StringHelpers.StringIsNullOrEmpty(valueFromDto) || targetProperty == null) continue;
            
            targetProperty.SetValue(existentNovel, valueFromDto);
        }
        
        await _novelRepository.UpdateNovel(existentNovel);
        return existentNovel;
    }
}