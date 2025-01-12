namespace backendpl.Services.ValidationService;

public interface IValidationBehavior<T>
{
    void Validate(T dtoToValidate);
}
