using backend.Entities.Dto;
using backend.Services.ErrorService;
using backend.Validators;
using FluentValidation;

namespace backend.Services.ValidationService;

public class NovelValidationService:INovelValidationService
{
    private readonly IValidator<CreateNovelDto> _validatorCreateNovelDto;
    private readonly IValidator<UpdateNovelDto> _validatorUpdateNovelDto;
    private readonly IValidator<IPagination> _validatorPagination;
    private readonly IErrorService _errorService;

    public NovelValidationService(IValidator<CreateNovelDto> validatorCreateNovelDto, IErrorService errorService, IValidator<UpdateNovelDto> validatorUpdateNovelDto, IValidator<IPagination> validatorPagination)
    {
        _validatorCreateNovelDto = validatorCreateNovelDto;
        _errorService = errorService;
        _validatorUpdateNovelDto = validatorUpdateNovelDto;
        _validatorPagination = validatorPagination;
    }
    
    public void ValidateCreate(CreateNovelDto createCreateNovelDto)
    {
        var  validationResult =  _validatorCreateNovelDto.Validate(createCreateNovelDto);
        if (!validationResult.IsValid)
        {
            var errors = _errorService.SanitazeError(validationResult.Errors);
            throw new ErrorCustomException(errors);
        }
    }

    public void ValidadeUpdate(UpdateNovelDto updateNovelDto)
    {
        var  validationResult =  _validatorUpdateNovelDto.Validate(updateNovelDto);
        if (!validationResult.IsValid)
        {
            var errors = _errorService.SanitazeError(validationResult.Errors);
            throw new ErrorCustomException(errors);
        }
    }

    public void ValidadePagination(IPagination pagination)
    {
        var  validationResult =  _validatorPagination.Validate(pagination);
        if (!validationResult.IsValid)
        {
            var errors = _errorService.SanitazeError(validationResult.Errors);
            throw new ErrorCustomException(errors);
        }
    }
    
    public bool StringIsNotNullNorEmpty(object? value)
    {
        return (value != null && !(value is string str && str == ""));
    }
} 