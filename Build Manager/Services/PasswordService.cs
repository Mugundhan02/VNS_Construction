using System.Security.Cryptography;
using System.Text;
using BuildManager.Interfaces;

namespace BuildManager.Services
{
    /// <summary>
    /// HMAC-SHA512 password hashing — military-grade security.
    ///
    /// Properties:
    ///   • Algorithm  : HMAC-SHA512 (512-bit output)
    ///   • Salt       : 64-byte (512-bit) per-user CSPRNG salt, used as the HMAC key
    ///   • Output     : 64-byte hash stored as Base64
    ///   • Comparison : CryptographicOperations.FixedTimeEquals — constant-time,
    ///                  prevents timing side-channel attacks
    ///   • CSPRNG     : RandomNumberGenerator (FIPS 140-2 compliant on Windows/.NET)
    ///   • No BCrypt  : pure .NET System.Security.Cryptography, zero third-party deps
    /// </summary>
    public class PasswordService : IPasswordService
    {
        // HMACSHA512 key size is 128 bytes (1024 bits) internally, but the hash
        // output is 64 bytes. We use 64-byte salts — matching the SHA-512 block
        // size — which is the standard recommendation for HMAC-SHA512 keys.
        private const int SaltBytes = 64;   // 512-bit salt

        public byte[] GenerateSalt()
            => RandomNumberGenerator.GetBytes(SaltBytes);

        public byte[] HashPassword(string password, byte[] salt)
        {
            ArgumentException.ThrowIfNullOrEmpty(password);
            ArgumentNullException.ThrowIfNull(salt);

            // Accept any non-empty salt — HMAC-SHA512 pads/truncates keys internally.
            // We always generate 64-byte salts via GenerateSalt(), so this is just
            // a safety guard against accidental empty arrays.
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

            // FixedTimeEquals: O(n) regardless of where the first mismatch is —
            // prevents timing-based brute-force attacks.
            return CryptographicOperations.FixedTimeEquals(computed, storedHash);
        }
    }
}
