using backend.Entities.Dto;
using backend.Services.ErrorService;
using FluentValidation;

namespace backend.Services.ValidationService;

public class UserValidationService: IUserValidationService
{
    private readonly IValidator<CreateUserDto> _createUserValidator;
    private readonly IErrorService _errorService;
    public UserValidationService(IValidator<CreateUserDto> createUserValidator, IErrorService errorService)
    {
        _createUserValidator = createUserValidator;
        _errorService = errorService;
    }
    
    public void ValidateCreate(CreateUserDto createUserDto)
    {
        var  validationResult =  _createUserValidator.Validate(createUserDto);
        if (!validationResult.IsValid)
        {
            var errors = _errorService.SanitazeError(validationResult.Errors);
            throw new ErrorCustomException(errors);
        }
    }
    
}