using backend.Data.Repository;
using backend.Entities;
using backend.Entities.Dto;
using backend.Services.ValidationService;
using backend.Utils;
using Microsoft.AspNetCore.Identity;

namespace backend.Services.AuthDomain.CreateUser;

public class CreateUserUseCase: ICreateUserUseCase
{
    private readonly IUserValidationService _userValidationService;
    private readonly IUserRepository _userRepository;
    private readonly PasswordHasher<User> _passwordHasher;
    
    public CreateUserUseCase(IUserValidationService userValidationService, IUserRepository userRepository)
    {
        _userValidationService = userValidationService;
        _userRepository = userRepository;
        _passwordHasher = new PasswordHasher<User>();
    }
    
    public async Task<User> Execute(CreateUserDto createUserDto)
    {
        _userValidationService.ValidateCreate(createUserDto);
        
        var existentUser = await _userRepository.FindUserByEmailOrUsermail(createUserDto.Email, createUserDto.Username);
        
        if (existentUser != null)
        {
            throw new Exception("User already existed");
        }
        
        var newUser = new User()
        {
            Email = createUserDto.Email,
            Password = createUserDto.Password,
            Username = createUserDto.Username,
        };
        
        newUser.Password = _passwordHasher.HashPassword(newUser, createUserDto.Password);
        
        await _userRepository.Add(newUser);

        return newUser;
    }
}