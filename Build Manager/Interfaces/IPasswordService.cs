namespace BuildManager.Interfaces
{
    /// <summary>
    /// HMAC-SHA512 password hashing service.
    /// Uses a unique 64-byte cryptographic salt per user as the HMAC key.
    /// </summary>
    public interface IPasswordService
    {
        /// <summary>
        /// Generates a cryptographically secure random 64-byte salt.
        /// Call once per user on registration or password change.
        /// </summary>
        byte[] GenerateSalt();

        /// <summary>
        /// Computes HMAC-SHA512(password, salt).
        /// Returns the raw 64-byte hash.
        /// </summary>
        byte[] HashPassword(string password, byte[] salt);

        /// <summary>
        /// Verifies a plain-text password against a stored HMAC-SHA512 hash.
        /// Uses a constant-time comparison to prevent timing attacks.
        /// </summary>
        bool VerifyPassword(string password, byte[] storedHash, byte[] salt);
    }
}
