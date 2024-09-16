namespace HMS.Abstractions
{
    public interface IAuthtenticationService
    {
        string GenerateSalt(int size = 32);
        string HashPassword(string password, string salt, string pepper);
        string GeneratePepper(int size = 32);


    }
}
