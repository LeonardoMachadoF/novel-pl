using backend.Entities.Dto;

namespace backend.Services.ValidationService;

public interface IUserValidationService
{
    void ValidateCreate(CreateUserDto userDto);
}