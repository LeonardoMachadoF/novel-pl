using backend.Data.Repository;
using backend.Entities;
using backend.Services.AuthDomain.Login;
using backend.Utils;
using Microsoft.AspNetCore.Identity;

namespace backend.Services.AuthDomain;

public class LoginUseCase:ILoginUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenGenerator _tokenGenerator;

    public LoginUseCase(IUserRepository userRepository, ITokenGenerator tokenGenerator)
    {
        _userRepository = userRepository;
        _tokenGenerator = tokenGenerator;
    }
    
    public async Task<string> Execute(string username, string password)
    {
        var user = await _userRepository.FindUserByEmailOrUsermail("", username);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        var passwordHasher = new PasswordHasher<User>();
        var verificationResult = passwordHasher.VerifyHashedPassword(user, user.Password, password);
        
        if (verificationResult == PasswordVerificationResult.Success)
        {
            return _tokenGenerator.GenerateToken(user);
        }

        return "";
    }
}