namespace BuildManager.Models
{
    /// <summary>
    /// Stores refresh tokens for authenticated users.
    /// Allows obtaining a new JWT without re-entering credentials.
    /// </summary>
    public class RefreshToken
    {
        public int    RefreshTokenId { get; set; }
        public int    CompanyUserId  { get; set; }
        public string Token          { get; set; } = string.Empty;
        public DateTime ExpiresAt    { get; set; }
        public DateTime CreatedAt    { get; set; } = DateTime.UtcNow;
        public bool   IsRevoked      { get; set; } = false;

        // Navigation
        public CompanyUser CompanyUser { get; set; } = null!;
    }
}
