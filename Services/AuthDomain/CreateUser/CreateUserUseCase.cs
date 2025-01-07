using backend.Data.Repository;
using backend.Entities;
using backend.Entities.Dto;
using backend.Services.ValidationService;
using backend.Utils;

namespace backend.Services.AuthDomain.CreateUser;

public class CreateUserUseCase: ICreateUserUseCase
{
    private readonly IUserValidationService _userValidationService;
    private readonly IUserRepository _userRepository;
    
    public CreateUserUseCase(IUserValidationService userValidationService, IUserRepository userRepository, TokenGenerator tokenGenerator)
    {
        _userValidationService = userValidationService;
        _userRepository = userRepository;
    }
    
    public async Task<User> Execute(CreateUserDto createUserDto)
    {
        _userValidationService.ValidateCreate(createUserDto);

        var newUser = new User()
        {
            Email = createUserDto.Email,
            Password = createUserDto.Password,
            Username = createUserDto.Username,
        };
        
        await _userRepository.Add(newUser);

        return newUser;
    }
}