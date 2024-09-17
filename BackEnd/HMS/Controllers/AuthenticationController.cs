using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using HMS.Abstractions;
using HMS.ViewModels;
using HMS.Models;
using HMS.DTO_s;
using Microsoft.AspNetCore.Authentication;

namespace HMS.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUsersService _userService;
        private readonly IConfigurationService _configurationService;
        private readonly IAuthtenticationService _authenticationService;
        private readonly IConfiguration _configuration;
        private readonly IUploadPictureService _uploadPictureServic;
        private readonly ILogger<AuthenticationController> _logger;


        public AuthenticationController(
            IUsersService userService,
            IConfigurationService configurationService,
            IAuthtenticationService authenticationService,
            IConfiguration configuration,
            IUploadPictureService uploadPictureServic)


        {
            _userService = userService;
            _configurationService = configurationService;
            _authenticationService = authenticationService;
            _configuration = configuration;
            _uploadPictureServic = uploadPictureServic;
        }

        public IActionResult Login() => View();

        public IActionResult Signup() => View();

        [HttpPost]
        public IActionResult Signup(UserVM user, IFormFile ProfilePicture)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
            {
                ModelState.AddModelError("", "Username and Password are required.");
                return View(user);
            }

            // Check if the username already exists
            if (_userService.IsUserExist(user.UserName))
            {
                ModelState.AddModelError("", "Username already taken.");
                return View(user);
            }
            string pictureName = _uploadPictureServic.UploadPicture(ProfilePicture);
            var salt = _authenticationService.GenerateSalt();
            var pepper = _configuration["EnvironmentVariables:PEPPER_VALUE"];
            var hashedPassword = _authenticationService.HashPassword(user.Password, salt, pepper);

            user.PasswordSalt = salt;
            user.PasswordHash = hashedPassword;
            user.ProfilePicture = pictureName;
            _userService.Register(user);

            return RedirectToAction("Login");
        }

    
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            var user = _userService.GetUserByUserName(model.EmailOrUserName);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(model);
            }

            var pepper = _configuration["EnvironmentVariables:PEPPER_VALUE"];
            var hashedPassword = _authenticationService.HashPassword(model.Password, user.PasswordSalt, pepper);

            if (user.PasswordHash != hashedPassword)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(model);
            }

            // Set up the claims
            List<Claim> claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Country, "Pakistan"),
    };

            // Create claims identity
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Authentication properties
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = model.KeepLoggedIn,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30) // Set an appropriate expiration time
            };

            // Sign in the user
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), properties);

            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
