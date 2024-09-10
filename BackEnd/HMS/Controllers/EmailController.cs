using HMS.Abstractions;
using HMS.DTO_s;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    public class EmailController : Controller
    {
        private IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost(Name = "SendEmail")]
        public void SendEmail(SendEmailDto emailDto)
        {
            emailDto.To = "ghufranullahkhan786@gmail.com";
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
        }
    }
}
