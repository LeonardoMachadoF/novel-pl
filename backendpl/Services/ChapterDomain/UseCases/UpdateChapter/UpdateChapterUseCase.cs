using backend.Data.Repository;
using backend.Entities;
using backend.Entities.Dto;
using backend.Utils;
using backendpl.Services.ValidationService;

namespace backend.Services.ChapterDomain.UseCases.UpdateChapter;

public class UpdateChapterUseCase : IUpdateChapterUseCase
{
    private readonly IChapterRepository _chapterRepository;
    private readonly IValidationBehavior<UpdateChapterDto> _validationBehavior;

    public UpdateChapterUseCase(IChapterRepository chapterRepository,
        IValidationBehavior<UpdateChapterDto> validationBehavior)
    {
        _chapterRepository = chapterRepository;
        _validationBehavior = validationBehavior;
    }


    public async Task<Chapter> Execute(UpdateChapterDto chapterDto, Guid Id)
    {
        _validationBehavior.Validate(chapterDto);

        var existentChapter = await _chapterRepository.GetChapterById(Id);
        if (existentChapter == null)
            throw new Exception("Capitulo não encontrado");

        var proprerties = chapterDto.GetType().GetProperties();

        foreach (var proprerty in proprerties)
        {
            var valueFromDto = proprerty.GetValue(chapterDto);
            var targetProperty = existentChapter.GetType().GetProperty(proprerty.Name);

            if (StringHelpers.StringIsNullOrEmpty(valueFromDto) || targetProperty == null) continue;
            targetProperty.SetValue(existentChapter, valueFromDto);
        }

        await _chapterRepository.UpdateChapter(existentChapter);
        return existentChapter;
    }
}