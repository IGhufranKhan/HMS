namespace HMS.ViewModels;

public class LoginVM
{
    public string EmailOrUserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool KeepLoggedIn { get; set; } 
    
}