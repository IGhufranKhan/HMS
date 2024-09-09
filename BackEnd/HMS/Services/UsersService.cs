using HMS.Abstractions;
using HMS.Models;

namespace HMS.Services;

public class UsersService : IUsersService
{
    private readonly HmsContext _context;

    public UsersService(HmsContext context)
    {
        _context = context;
    }

    public User? GetUser(string emailOrUsername, string password)
    {
        return _context
            .Users
            .FirstOrDefault(x => x.UserName == emailOrUsername && x.PasswordHash == password);
    }

    public void Register(User user)
    {
        user.UserId = Guid.NewGuid();

        _context.Users.Add(user);

        _context.SaveChanges();
    }
}
