using HMS.Models;
using HMS.ViewModels;

namespace HMS.Abstractions;

public interface IUsersService
{
    void Register(UserVM user);
    User? GetUserByUserName(string emailOrUsername);
    bool IsUserExist(string userName);
    void Update(User user);
    User? GetUserById(Guid Id);
}
