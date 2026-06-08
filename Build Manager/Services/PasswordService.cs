using System.Security.Cryptography;
using BuildManager.Interfaces;

namespace BuildManager.Services
{
    public class PasswordService : IPasswordService
    {
        public Task<byte[]> HashPassword(string password, byte[] key)
        {
            using var hmac = new HMACSHA512(key);
            var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return Task.FromResult(hash);
        }

        public Task<bool> VerifyPassword(string password, byte[] hashedPassword, byte[] key)
        {
            using var hmac = new HMACSHA512(key);
            var computed = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return Task.FromResult(computed.SequenceEqual(hashedPassword));
        }
    }
}
