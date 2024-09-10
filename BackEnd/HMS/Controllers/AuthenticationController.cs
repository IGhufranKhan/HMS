using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using HMS.Abstractions;
using HMS.ViewModels;
using HMS.Models;
using Microsoft.AspNetCore.Authorization;
using HMS.DTO_s;

namespace HMS.Controllers;

public class AuthenticationController : Controller
{
    private readonly IUsersService _userService;
    private readonly IEmailService _emailService;

    private readonly IConfigurationService _configurationService;
    public AuthenticationController(
        IUsersService userService,
        IConfigurationService configurationService,
        IEmailService emailService)
    {
        _userService = userService;
        _configurationService = configurationService;
        _emailService = emailService;
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
    public IActionResult Signup(User user)
    {
        _userService.Register(user);

        return RedirectToAction("Login");
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM model)
    {

        User? user = _userService.GetUser(model.EmailOrUserName, model.PassWord);

        if (user != null)
        {
            int expiryTimeFram = _configurationService.GetExpiryTimeframeInMinutes();

            List<Claim> claims = new List<Claim>() 
            {
                new Claim(ClaimTypes.NameIdentifier, model.EmailOrUserName),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Country, "Pakistan"),
                new Claim(ClaimTypes.Sid,user.UserId.ToString())

            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = model.KeepLoggedIn,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(expiryTimeFram)
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), properties);
            SendEmailDto emailDto = new SendEmailDto();
            emailDto.To = user.Email;
            emailDto.Subject = "Login Email";
            emailDto.PlainText = "Congratulations! You have successfully logged in to HMS.";

            emailDto.Html = @"<html>
    <body>
        <h1>Congratulations!</h1>
        <p>You have successfully logged in to <strong>HMS</strong>.</p>
        <p>If you did not perform this login, please contact support immediately.</p>
        <br/>
        <p>Best regards,</p>
        <p>The HMS Team</p>
    </body>
    </html>";
            _emailService.SendEmail(emailDto);
            return RedirectToAction("Index", "Home");
        }
      
        ViewData["ValidateMessage"] = "User not found";

        return View();
    }

    public async Task<IActionResult> LogOut()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Login", "Authentication");
    }
}
