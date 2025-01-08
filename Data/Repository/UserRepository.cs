using backend.Entities;
using backend.Entities.Dto;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Repository;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;
    
    public UserRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task Add(User user)
    {
        _context.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User> AutenticateUser(string username, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username && x.Password == password);
        return user;
    }

    public async Task<User> FindUserByEmailOrUsermail(string email, string usermail)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email || x.Username == usermail);
        return user;
    }

    public async Task<User> FindUserInfo(string username)
    {
        var user = await _context.Users.Include(x=>x.Novels).FirstOrDefaultAsync(a => a.Username == username);
        return user;
    }

    public async Task AddFavorite(User user, Novel novel)
    {
        user.Novels.Add(novel);
        await _context.SaveChangesAsync();
    }
}