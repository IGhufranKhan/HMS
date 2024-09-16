using System;
using System.Collections.Generic;

namespace HMS.ViewModels;

public class UserVM
{
    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string PasswordSalt { get; set; } = null!;

    public string Password { get; set; } = null!;
    public string ProfilePicture { get; set; } 
    public int? Age { get; set; } 
}
