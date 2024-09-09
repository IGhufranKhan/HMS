namespace HMS.Abstractions;

public interface IContextService
{
    bool IsUserLoggedIn();
    string GetUserId();
    string GetUserName();
}
