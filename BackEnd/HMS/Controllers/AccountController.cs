using HMS.Abstractions;
using HMS.Models;
using HMS.Services;
using HMS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HMS.Controllers
{

    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUsersService _userService;
        private readonly IContextService _contextService;
        private readonly IUploadPictureService _uploadPictureService;


        public AccountController(IUsersService userService, IContextService contextService, IUploadPictureService uploadPictureService)
        {
            _userService = userService;
            _contextService = contextService;
            _uploadPictureService = uploadPictureService;
        }

        public IActionResult Index()
        {
            var userId = _contextService.GetUserId();
            var user = _userService.GetUserById(Guid.Parse(userId));
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
            var userId = _contextService.GetUserId();
            var user = _userService.GetUserById(Guid.Parse(userId)); 
            var model = new AccountViewModel
            {
                Id = user.UserId,
                Name = user.FirstName + " " + user.LastName,
                Age = user.Age,
                Email = user.Email,
                ProfilePicture =_contextService.GetUserProfilePicture() // Path to the profile picture
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
                return RedirectToAction("Manage");

            }
            
            return View("Manage", model);
        }
        // Post: Account/UploadPicture
        [HttpPost]
        public IActionResult UploadPicture(IFormFile profilePicture)
        {
            if (profilePicture != null && profilePicture.Length > 0)
            {
                var fileName = _uploadPictureService.UploadPicture(profilePicture);
                _contextService.updateProfilePicture(fileName);

                // Update the user's profile picture
                //_userService.ProfilePicture = "/images/" + fileName;
            }

            return RedirectToAction("Manage");
        }
        [HttpPost]
        public IActionResult RemovePicture(Guid Id, IFormFile profilePicture)
        {
            if (Id != Guid.Empty)
            {
                var user = _userService.GetUserById(Id);

                if (user != null)
                {
                    // Remove old picture if needed
                    if (!string.IsNullOrEmpty(user.ProfilePicture))
                    {
                        var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", user.ProfilePicture);
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                        user.ProfilePicture = null;
                        _userService.Update(user);
                    }

                }
            }
            return RedirectToAction("Manage");

        }


    }
    
}
