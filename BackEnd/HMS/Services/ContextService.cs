using HMS.Abstractions;
using HMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Security.Claims;

namespace HMS.Services;

public class ContextService : IContextService
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IUsersService _usersService;
    private readonly HmsContext _context;

    public ContextService(IHttpContextAccessor contextAccessor, IUsersService usersService, HmsContext context)
    {
        _contextAccessor = contextAccessor;
        _usersService = usersService;
        _context = context;
    }

    public string GetUserId()
    {
        var user = _contextAccessor.HttpContext?.User;

        if (user != null && user.Identity!.IsAuthenticated)
        {
           string Id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
           
            return Id;
        }
        return "";
    }

    public string GetUserName()
    {
        var user = _contextAccessor.HttpContext?.User;

        if (user != null && user.Identity!.IsAuthenticated)
        {
            string name = user.FindFirst(ClaimTypes.Name)?.Value!;
            return name;
        }
        return "";
    }

    public bool IsUserLoggedIn()
    {
        return _contextAccessor?
            .HttpContext?
            .User?
            .Identity?
            .IsAuthenticated ?? true;
    }
    public string GetUserProfilePicture()
    {
        string id = GetUserId();
        if (id != null)
        {
            var user = _usersService.GetUserById(Guid.Parse(id));
            if (!string.IsNullOrEmpty(user.ProfilePicture)) 
            {
                string picture = "/uploads/" + user.ProfilePicture;
                return picture;
            }
        }
        return "/uploads/default.png";
    }
 
    public void updateProfilePicture(string picture)
    {
        string id = GetUserId();
        if (id != null)
        {
            var user = _usersService.GetUserById(Guid.Parse(id));
            if(user !=null && !string.IsNullOrEmpty(picture))
            {
                user.ProfilePicture = picture;
                _context.Users.Update(user);
                _context.SaveChanges();
            }
            
        }
    }
}
