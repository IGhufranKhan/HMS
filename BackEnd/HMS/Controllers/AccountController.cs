using HMS.Abstractions;
using HMS.Services;
using HMS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HMS.Controllers
{
    
    
    public class AccountController : Controller
    {
        private readonly IUsersService _userService;
        public AccountController(IUsersService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _userService.GetUserByUserName(userId);
            var model = new AccountViewModel
            {
                Id = user.UserId,
                Name = user.FirstName + " " + user.LastName,
                Age = user.Age,
                Email = user.Email,
                ProfilePicture = user.ProfilePicture!
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult Manage()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _userService.GetUserByUserName(userId);
            var model = new AccountViewModel
            {
                Id = user.UserId,
                Name = user.FirstName + " " + user.LastName,
                Age = user.Age,
                Email = user.Email,
                ProfilePicture = user.ProfilePicture!
            };
            return View(model);
        }

        // Post: Account/UpdateProfile
        [HttpPost]
        public IActionResult UpdateProfile(AccountViewModel model)
        {
            var user = _userService.GetUserById(model.Id);
            if (user != null) 
            {
                var name = model.Name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                user.FirstName = name[0];
                user.LastName = name.Length > 1 ? name[1] : string.Empty;
                user.Age = model.Age;
                user.Email = model.Email;
                _userService.Update(user);
                return RedirectToAction("Index");

            }
            
            return View("Index", model);
        }
        // Post: Account/UploadPicture
        [HttpPost]
        public IActionResult UploadPicture(IFormFile profilePicture)
        {
            if (profilePicture != null && profilePicture.Length > 0)
            {
                var fileName = Path.GetFileName(profilePicture.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    profilePicture.CopyTo(stream);
                }

                // Update the user's profile picture
                //_userService.ProfilePicture = "/images/" + fileName;
            }

            return RedirectToAction("Index");
        }



    }
    //[HttpPost]
    //public IActionResult RemovePicture()
    //{
    //    // Set the profile picture to a default image
    //    _userService.ProfilePicture = "/images/default-profile.jpg";
    //    return _userService("Manage");
    //}
}
