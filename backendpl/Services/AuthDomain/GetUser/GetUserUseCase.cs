using backend.Data.Repository;
using backend.Entities;

namespace backend.Services.AuthDomain.GetUser;

public class GetUserUseCase: IGetUserUseCase
{
    private readonly IUserRepository _userRepository;
    public GetUserUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<User> Execute(string username)
    {
        var user = await _userRepository.FindUserInfo(username);
        return user;
    }
}