using System.Security.Cryptography;
using System.Text;
using BuildManager.Interfaces;

namespace BuildManager.Services
{
    public class PasswordService : IPasswordService
    {
        private const int SaltBytes = 64;

        public byte[] GenerateSalt()
        {
            return RandomNumberGenerator.GetBytes(SaltBytes);
        }

        public byte[] HashPassword(string password, byte[] salt)
        {
            ArgumentException.ThrowIfNullOrEmpty(password);
            ArgumentNullException.ThrowIfNull(salt);

            if (salt.Length == 0)
                throw new ArgumentException("Salt must not be empty.", nameof(salt));

            using var hmac = new HMACSHA512(salt);
            return hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public bool VerifyPassword(string password, byte[] storedHash, byte[] salt)
        {
            if (string.IsNullOrEmpty(password) || storedHash is null || salt is null)
                return false;

            if (storedHash.Length == 0 || salt.Length == 0)
                return false;

            var computed = HashPassword(password, salt);

            return CryptographicOperations.FixedTimeEquals(computed, storedHash);
        }
    }
}