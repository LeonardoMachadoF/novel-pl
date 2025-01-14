using Microsoft.AspNetCore.Identity;

namespace backend.Entities;

public class User
{
    public User()
    {
        UserId = Guid.NewGuid();
        Role = "User";
    }

    public Guid UserId { get; set; }

    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public string Role { get; set; }
    public List<Novel> Novels { get; set; }
}