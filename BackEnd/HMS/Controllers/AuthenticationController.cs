using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using HMS.Abstractions;
using HMS.ViewModels;
using HMS.Models;
using HMS.DTO_s;


namespace HMS.Controllers;

public class AuthenticationController : Controller
{
    private readonly IUsersService _userService;
    private readonly IEmailService _emailService;

    private readonly IConfigurationService _configurationService;
    private readonly IAuthtenticationService _authenticationService;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthenticationController> _logger;

    public AuthenticationController(
        IUsersService userService,
        IConfigurationService configurationService,
        IEmailService emailService,
        IAuthtenticationService authenticationService,
        IConfiguration configuration,
        ILogger<AuthenticationController> logger)
    {
        _userService = userService;
        _configurationService = configurationService;
        _emailService = emailService;
        _authenticationService = authenticationService;
        _configuration = configuration;
        _logger = logger;
    }

    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Signup()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Signup(UserVM user)
    {
        if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
        {
            ModelState.AddModelError("", "Username and Password are required.");
            return View(user); // return the same view with error message
        }

        // Check if the username already exists
        if (_userService.IsUserExist(user.UserName))
        {
            ModelState.AddModelError("", "Username already taken.");
            return View(user); // return the same view with error message
        }
        var salt = _authenticationService.GenerateSalt();
        var pepper = _configuration["EnvironmentVariables:PEPPER_VALUE"];
        var hashedPassword = _authenticationService.HashPassword(user.Password, salt, pepper);
        user.PasswordSalt = salt;
        user.PasswordHash = hashedPassword;
        _userService.Register(user);
        return RedirectToAction("Login");


    }

    [HttpPost]
    public IActionResult Login(LoginVM model)
    {
        // Check if the username or email exists in the database
        User? user = _userService.GetUserByUserName(model.EmailOrUserName);
        if (user == null)
        {
            ModelState.AddModelError("", "Invalid username or password.");
            return View(model); // Return model to repopulate form
        }

        // Get pepper value from configuration
        var pepper = _configuration["EnvironmentVariables:PEPPER_VALUE"];
        if (string.IsNullOrEmpty(pepper))
        {
            ModelState.AddModelError("", "Server configuration error. Please try again later.");
            return View(model);
        }

        // Hash the provided password with salt and pepper
        var hashedPassword = _authenticationService.HashPassword(model.Password, user.PasswordSalt, pepper);

        // Check if the hashed password matches the stored password hash
        if (user.PasswordHash != hashedPassword)
        {
            ModelState.AddModelError("", "Invalid username or password.");
            return View(model);
        }
        // Prepare the email for login notification
        try
        {
            SendEmailDto emailDto = new SendEmailDto
            {
                To = user.Email,
                Subject = "Login Email",
                PlainText = "Congratulations! You have successfully logged in to HMS.",
                Html = @"<html>
                        <body>
                            <h1>Congratulations!</h1>
                            <p>You have successfully logged in to <strong>HMS</strong>.</p>
                            <p>If you did not perform this login, please contact support immediately.</p>
                            <br/>
                            <p>Best regards,</p>
                            <p>The HMS Team</p>
                        </body>
                    </html>"
            };

            // Send the email
            //_emailService.SendEmail(emailDto);
        }
        catch (Exception ex)
        {
            // Log email sending failure and continue (non-critical)
            _logger.LogError("Failed to send login email: " + ex.Message);
        }
        // Authentication is successful, proceed to sign-in
        int expiryTimeFram = _configurationService.GetExpiryTimeframeInMinutes();

        // Create user claims
        List<Claim> claims = new List<Claim>()
    {
        new Claim(ClaimTypes.NameIdentifier, model.EmailOrUserName),
        new Claim(ClaimTypes.GivenName, user.FirstName ?? string.Empty),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Country, "Pakistan"),
        new Claim(ClaimTypes.Sid, user.UserId.ToString())
    };

        // Create claims identity
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        // Authentication properties
        AuthenticationProperties properties = new AuthenticationProperties()
        {
            AllowRefresh = true,
            IsPersistent = model.KeepLoggedIn,
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(expiryTimeFram)
        };

        // Sign in the user
         HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity), properties);

        

        // Redirect to home page after successful login
        return RedirectToAction("Manage", "Account");
    }


    public async Task<IActionResult> LogOut()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Login", "Authentication");
    }
}
