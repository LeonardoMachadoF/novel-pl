using backend.Data.Repository;
using backend.Entities;
using backend.Entities.Dto;
using backend.Services.ValidationService;
using backend.Utils;

namespace backend.Services.ChapterDomain.UseCases.UpdateChapter;

public class UpdateChapterUseCase: IUpdateChapterUseCase
{
    private readonly IChapterRepository _chapterRepository;
    private readonly IChapterValidationService _validationService;
    
    public UpdateChapterUseCase(IChapterRepository chapterRepository, IChapterValidationService validationService, INovelValidationService novelValidationService)
    {
        _chapterRepository = chapterRepository;
        _validationService = validationService;
    }


    public async Task<Chapter> Execute(UpdateChapterDto chapterDto, Guid Id)
    {
        _validationService.ValidateUpdate(chapterDto);
        
        var existentChapter = await _chapterRepository.GetChapterById(Id);
        if (existentChapter == null)
            throw new Exception("Capitulo não encontrado");

        var proprerties = chapterDto.GetType().GetProperties();
        
        foreach (var proprerty in proprerties)
        {
            var valueFromDto = proprerty.GetValue(chapterDto);
            var targetProperty = existentChapter.GetType().GetProperty(proprerty.Name);

            if (StringHelpers.StringIsNullorEmpty(valueFromDto) || targetProperty == null) continue;
            targetProperty.SetValue(existentChapter, valueFromDto);
        }
        
        await _chapterRepository.UpdateChapter(existentChapter);
        return existentChapter;
    }
}