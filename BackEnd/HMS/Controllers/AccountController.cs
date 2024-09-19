using HMS.Abstractions;
using HMS.DTO_s;
using HMS.Models;
using HMS.Services;
using HMS.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;


namespace HMS.Controllers
{

    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUsersService _userService;
        private readonly IContextService _contextService;
        private readonly IUploadPictureService _uploadPictureService;
        private readonly IEmailService _emailService;
        private readonly IAuthtenticationService _authenticationService;
        private readonly IConfiguration _configuration;
        public AccountController(IUsersService userService, IContextService contextService, IUploadPictureService uploadPictureService, IAuthtenticationService authenticationService, IConfiguration configuration, IEmailService emailService)
        {
            _userService = userService;
            _contextService = contextService;
            _uploadPictureService = uploadPictureService;
            _authenticationService = authenticationService;
            _configuration = configuration;
            _emailService = emailService;
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

        // Action to send the OTP
        [HttpPost]
        public async Task<IActionResult> SendOtp()
        {
            var user = _contextService.GetUser();
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            // Generate a 6-digit OTP
            var otp = new Random().Next(100000, 999999).ToString();

            // Store the OTP in TempData for validation later
            TempData["OTP"] = otp;

            // Send the OTP to the user's email
            var model = new SendEmailDto();
            model.PlainText = "your otp code";
            model.Html = $"Your OTP is: {otp}";
            model.Subject = "OTP for reset password";
            model.To = user.Email;
             _emailService.SendEmail(model);

            // Show the OTP input modal
            return PartialView("_OtpModal");
        }

        // Action to verify the OTP
        [HttpPost]
        public bool VerifyOtp(string otp)
        {
            var storedOtp = TempData["OTP"]?.ToString();

            if (storedOtp != null && storedOtp == otp)
            {
                // If OTP is valid, allow the user to change the password
                TempData["OtpVerified"] = true;
                return true;
            }
            return false;
            
            
            
        }

        // Action to change the password after OTP verification
        
        
        [HttpGet]
        public IActionResult PasswordChange()
        {
            // Ensure the user has verified the OTP before allowing password change.
            if (TempData["OtpVerified"] != null && (bool)TempData["OtpVerified"])
            {
                return View(new ChangePasswordViewModel());
            }

            // If OTP is not verified, redirect to some appropriate page (e.g., OTP request page).
            return View(new ChangePasswordViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PasswordChange(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // If the model state is invalid, return the view with the validation errors.
                return View(model);
            }

            // Ensure the user has verified the OTP before allowing password change.
            bool isOtpverfied = VerifyOtp(model.Otp);
            if (TempData["OtpVerified"] == null || !(bool)TempData["OtpVerified"])
            {
                ModelState.AddModelError(string.Empty, "OTP verification is required.");
                return View(model);
            }

            // Get the current logged-in user (this depends on how you handle user management in your app).
            var user =  _contextService.GetUser();
            if (user == null)
            {
                return RedirectToAction("Login", "Authentication");
            }

            // Verify that the new password and confirm password match.
            if (model.NewPassword != model.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "The new password and confirmation password do not match.");
                return View(model);
            }

            // Hash the new password using ASP.NET Identity's password hasher.
            var salt = _authenticationService.GenerateSalt();
            var pepper = _configuration["EnvironmentVariables:PEPPER_VALUE"];
            var hashedPassword = _authenticationService.HashPassword(model.NewPassword, salt, pepper);

            user.PasswordSalt = salt;
            user.PasswordHash = hashedPassword;
           

            // Update the user's password in the database.
            bool result = _userService.IsUpdate(user);

            if (result)
            {
                // If the password update is successful, clear OTP verification and redirect to success page.
                TempData["OtpVerified"] = null;
                return RedirectToAction("PasswordChangeSuccess");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error changing password. Please try again.");
                return View(model);
            }
        }

        // Action to display success page after password change
        [HttpGet]
        public IActionResult PasswordChangeSuccess()
        {
            return View();
        }
    }

}

