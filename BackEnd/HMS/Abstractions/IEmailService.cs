using HMS.DTO_s;

namespace HMS.Abstractions
{
    public interface IEmailService
    {
        public void SendEmail(SendEmailDto emailDto);
    }
}
