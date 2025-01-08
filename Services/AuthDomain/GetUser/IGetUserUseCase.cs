using backend.Entities;

namespace backend.Services.AuthDomain.GetUser;

public interface IGetUserUseCase
{
    Task<User> Execute(string username);
}