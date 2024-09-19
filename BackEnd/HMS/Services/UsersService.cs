using HMS.Abstractions;
using HMS.Models;
using HMS.ViewModels;

namespace HMS.Services;

public class UsersService : IUsersService
{
    private readonly HmsContext _context;

    public UsersService(HmsContext context)
    {
        _context = context;
    }

    public User? GetUserByUserName(string emailOrUsername)
    {
        return _context
            .Users
            .FirstOrDefault(x => x.UserName == emailOrUsername || x.Email == emailOrUsername);
    }
    public bool IsUserExist(string userName)
    {
        var user = _context.Users.Where(x => x.UserName == userName).FirstOrDefault();
        if (user != null) 
        {
            return true;
        }
        return false;
    }
    public User? GetUserById(Guid Id)
    {
        return _context
            .Users
            .FirstOrDefault(x => x.UserId == Id);
    }

    public void Register(UserVM userVm)
    {
        var user = new User();
        if (userVm != null) 
        {
            user.UserId = Guid.NewGuid();
            user.UserName = userVm.UserName;
            user.PasswordHash = userVm.PasswordHash;
            user.Email = userVm.Email;
            user.PasswordSalt = userVm.PasswordSalt;
            user.FirstName = userVm.FirstName;
            user.LastName = userVm.LastName;
            user.CreatedAt = DateTime.Now.Date;
            user.IsActive = true;
            user.Age = userVm.Age;
            user.ProfilePicture = userVm.ProfilePicture;
            //user.UpdatedAt = DateTime.Now.Date;

        }
        

        _context.Users.Add(user);

        _context.SaveChanges();
    }
    public void Update(User user)
    {
        //var existintnguser = GetUserById(user.UserId);
        if (user != null)
        {
            user.UpdatedAt = DateTime.Now.Date;
            //user.UserId = Guid.NewGuid();
            //existintnguser.UserName = user.UserName;
            //existintnguser.PasswordHash = user.PasswordHash;
            //existintnguser.Email = user.Email;
            //existintnguser.PasswordSalt = user.PasswordSalt;
            //existintnguser.FirstName = user.FirstName;
            //existintnguser.LastName = user.LastName;
            ////existintnguser.CreatedAt = DateTime.Now.Date;
            //existintnguser.IsActive = true;
            //existintnguser.Age = user.Age;
            //existintnguser.ProfilePicture = user.ProfilePicture;
            //existintnguser.UpdatedAt = DateTime.Now.Date;
            _context.Users.Update(user);

            _context.SaveChanges();
        }
    }
        public bool IsUpdate(User user)
        {
            //var existintnguser = GetUserById(user.UserId);
            if (user != null)
            {
                user.UpdatedAt = DateTime.Now.Date;
                //user.UserId = Guid.NewGuid();
                //existintnguser.UserName = user.UserName;
                //existintnguser.PasswordHash = user.PasswordHash;
                //existintnguser.Email = user.Email;
                //existintnguser.PasswordSalt = user.PasswordSalt;
                //existintnguser.FirstName = user.FirstName;
                //existintnguser.LastName = user.LastName;
                ////existintnguser.CreatedAt = DateTime.Now.Date;
                //existintnguser.IsActive = true;
                //existintnguser.Age = user.Age;
                //existintnguser.ProfilePicture = user.ProfilePicture;
                //existintnguser.UpdatedAt = DateTime.Now.Date;
                _context.Users.Update(user);

                _context.SaveChanges();
            return true;
            }
            return false;
            

        }
}
