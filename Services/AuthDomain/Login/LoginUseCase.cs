using backend.Data.Repository;
using backend.Services.AuthDomain.Login;
using backend.Utils;

namespace backend.Services.AuthDomain;

public class LoginUseCase:ILoginUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly TokenGenerator _tokenGenerator;

    public LoginUseCase(IUserRepository userRepository, TokenGenerator tokenGenerator)
    {
        _userRepository = userRepository;
        _tokenGenerator = tokenGenerator;
    }
    
    public async Task<string> Execute(string username, string password)
    {
        var user = await _userRepository.AutenticateUser(username, password);
        if (user != null)
        {
            return _tokenGenerator.GenerateToken(user.UserId, user.Email);
        }
        return null;
    }
}