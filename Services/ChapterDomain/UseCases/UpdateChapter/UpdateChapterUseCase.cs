using backend.Data.Repository;
using backend.Entities;
using backend.Entities.Dto;
using backend.Services.ValidationService;

namespace backend.Services.ChapterDomain.UseCases.UpdateChapter;

public class UpdateChapterUseCase: IUpdateChapterUseCase
{
    private readonly IChapterRepository _chapterRepository;
    private readonly IChapterValidationService _validationService;
    private readonly INovelValidationService _novelValidationService;
    
    public UpdateChapterUseCase(IChapterRepository chapterRepository, IChapterValidationService validationService, INovelValidationService novelValidationService)
    {
        _chapterRepository = chapterRepository;
        _validationService = validationService;
        _novelValidationService = novelValidationService;
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

            if (!_novelValidationService.StringIsNotNullNorEmpty(valueFromDto) || targetProperty == null) continue;
            targetProperty.SetValue(existentChapter, valueFromDto);
        }
        
        await _chapterRepository.UpdateChapter(existentChapter);
        return existentChapter;
    }
}