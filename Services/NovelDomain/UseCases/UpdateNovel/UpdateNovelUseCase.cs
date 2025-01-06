using backend.Data.Repository;
using backend.Entities;
using backend.Entities.Dto;
using backend.Services.ValidationService;

namespace backend.Services.NovelServices.UseCases.UpdateNovel;

public class UpdateNovelUseCase: IUpdateNovelUseCase
{
    private readonly INovelRepository _novelRepository;
    private readonly INovelValidationService _validationService;

    public UpdateNovelUseCase(INovelRepository novelRepository, INovelValidationService validationService)
    {
        _novelRepository = novelRepository;
        _validationService = validationService;
    }
    
    public async Task<Novel> Execute(Guid id, UpdateNovelDto updateNovelDto)
    {
        _validationService.ValidadeUpdate(updateNovelDto);
        
        var existentNovel = await _novelRepository.GetNovelById(id);
        if (existentNovel == null)
            throw new Exception("Novel não encontrada");
        
        var novelDtoProperties = updateNovelDto.GetType().GetProperties();

        foreach (var property in novelDtoProperties)
        {
            var valueFromDto = property.GetValue(updateNovelDto);
            var targetProperty = existentNovel.GetType().GetProperty(property.Name);
            
            if (!_validationService.StringIsNotNullNorEmpty(valueFromDto) || targetProperty == null) continue;
            
            targetProperty.SetValue(existentNovel, valueFromDto);
        }
        
        await _novelRepository.UpdateNovel(existentNovel);
        return existentNovel;
    }
}