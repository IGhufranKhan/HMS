using HMS.Abstractions;
using System.Security.Cryptography;
using System.Text;

namespace HMS.Services
{
    public class AuthtenticationService : IAuthtenticationService
    {
        public string GenerateSalt(int size = 32)
        {
            var rng = new RNGCryptoServiceProvider();
            var saltBytes = new byte[size];
            rng.GetBytes(saltBytes);

            return Convert.ToBase64String(saltBytes);
        }
        public string HashPassword(string password, string salt, string pepper)
        {
            using var sha512 = SHA512.Create();

            // Combine password, salt, and pepper
            var passwordSaltPepper = $"{password}{salt}{pepper}";
            var byteValue = Encoding.UTF8.GetBytes(passwordSaltPepper);

            // Compute the hash
            var byteHash = sha512.ComputeHash(byteValue);

            // Convert the hash to a Base64 string
            return Convert.ToBase64String(byteHash);
        }
        public string GeneratePepper(int size = 32)
        {
            var rng = new RNGCryptoServiceProvider();
            var pepperBytes = new byte[size];
            rng.GetBytes(pepperBytes);

            return Convert.ToBase64String(pepperBytes);
        }
    }
}
