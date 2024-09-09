using HMS.Models;

namespace HMS.Abstractions;

public interface IUsersService
{
    void Register(User user);
    User? GetUser(string emailOrUsername, string password);
}
