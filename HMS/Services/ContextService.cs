using HMS.Abstractions;

namespace HMS.Services;

public class ContextService : IContextService
{
    private readonly IHttpContextAccessor _contextAccessor;
    public ContextService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public string GetUserId()
    {
        throw new NotImplementedException();
    }

    public string GetUserName()
    {
        throw new NotImplementedException();
    }

    public bool IsUserLoggedIn()
    {
        return _contextAccessor?
            .HttpContext?
            .User?
            .Identity?
            .IsAuthenticated ?? true;
    }
}
