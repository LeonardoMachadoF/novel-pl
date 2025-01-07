using backend.Entities;
using backend.Entities.Dto;

namespace backend.Data.Repository;

public interface IUserRepository
{
    Task Add(User user);
    Task<User> AutenticateUser(string username, string password);
    
    Task<User> FindUserByEmailOrUsermail(string email, string username);
}