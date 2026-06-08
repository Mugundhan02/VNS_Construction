namespace BuildManager.Models
{
    /// <summary>
    /// Represents a user of the application linked to a company.
    /// Roles: Owner | Admin | User
    /// </summary>
    public class CompanyUser
    {
        public int    CompanyUserId { get; set; }
        public int    CompanyId    { get; set; }
        public string UserName     { get; set; } = string.Empty;

        /// <summary>
        /// HMAC-SHA512 hash of the password, stored as Base64.
        /// Never store plain text.
        /// </summary>
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Cryptographically random 64-byte salt used as the HMAC key.
        /// Unique per user, stored as Base64.
        /// </summary>
        public string PasswordSalt { get; set; } = string.Empty;

        /// <summary>Role: Owner | Admin | User</summary>
        public string UserType { get; set; } = "User";

        public bool IsActive { get; set; } = true;

        // Navigation properties
        public Company                   Company       { get; set; } = null!;
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
