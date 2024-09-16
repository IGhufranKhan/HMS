namespace HMS.ViewModels
{
    public class AccountViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Email { get; set; }
        public string ProfilePicture { get; set; } // URL of profile picture
    }
}
