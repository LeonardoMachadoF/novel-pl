using backend.Services.ErrorService;
using backendpl.Services.ValidationService;
using FluentValidation;

public class ValidationBehavior<T> : IValidationBehavior<T>
{
    private readonly IValidator<T> _validatorCustom;
    private readonly IErrorService _errorService;
    
    public ValidationBehavior(IValidator<T> validatorCustom, IErrorService errorService)
    {
        _validatorCustom = validatorCustom;
        _errorService = errorService;
    }
    
    public void Validate(T dtoToValidate)
    {
        var  validationResult =  _validatorCustom.Validate(dtoToValidate);
        if (!validationResult.IsValid)
        {
            var errors = _errorService.SanitazeError(validationResult.Errors);
            throw new ErrorCustomException(errors);
        }
    }
    
}