using HMS.Models;

namespace HMS.Abstractions;

public interface IContextService
{
    bool IsUserLoggedIn();
    string GetUserId();
    string GetUserName();
    string GetUserProfilePicture();
    void updateProfilePicture(string picture);
    User GetUser();
}
