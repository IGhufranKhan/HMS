namespace HMS.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public bool IsOtpVerified { get; set; }
        public string Otp { get; set; }
    }

}
