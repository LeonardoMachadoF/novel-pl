using backend.Entities;
using backend.Entities.Dto;

namespace backend.Services.AuthDomain.CreateUser;

public interface ICreateUserUseCase
{
    Task<User> Execute(CreateUserDto userDto);
}