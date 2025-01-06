using backend.Entities;
using backend.Entities.Dto;
using backend.Services.ErrorService;
using FluentValidation;

namespace backend.Services.ValidationService;

public class ChapterValidationService: IChapterValidationService
{
    private readonly IValidator<CreateChapterDto> _chapterCreateValidator;
    private readonly IValidator<UpdateChapterDto> _chapterUpdateValidator;
    private readonly IErrorService _errorService;

    public ChapterValidationService(IValidator<CreateChapterDto> chapterCreateValidator ,IErrorService errorService, IValidator<UpdateChapterDto> chapterUpdateValidator)
    {
        _chapterCreateValidator = chapterCreateValidator;
        _errorService = errorService;
        _chapterUpdateValidator = chapterUpdateValidator;
    }

    public void ValidateCreate(CreateChapterDto createChapterDto)
    {
        var  validationResult =  _chapterCreateValidator.Validate(createChapterDto);
        if (!validationResult.IsValid)
        {
            var errors = _errorService.SanitazeError(validationResult.Errors);
            throw new ErrorCustomException(errors);
        }
    }
    
    public void ValidateUpdate(UpdateChapterDto updateChapterDto)
    {
        var validationResult = _chapterUpdateValidator.Validate(updateChapterDto);
        if (!validationResult.IsValid)
        {
            var errors = _errorService.SanitazeError(validationResult.Errors);
            throw new ErrorCustomException(errors);
        }
    }
}