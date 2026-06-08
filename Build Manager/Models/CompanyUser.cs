namespace BuildManager.Models
{
    /// <summary>
    /// Represents a user of the application linked to a company.
    /// Corresponds to the "Company User Details" screen.
    /// Roles: Owner, Admin, User.
    /// </summary>
    public class CompanyUser
    {
        public int CompanyUserId { get; set; }

        public int CompanyId { get; set; }

        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Hashed password — never store plain text.
        /// </summary>
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Role: Owner | Admin | User
        /// </summary>
        public string UserType { get; set; } = "User";

        public bool IsActive { get; set; } = true;

        // Navigation properties
        public Company Company { get; set; } = null!;
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
