using System.ComponentModel.DataAnnotations;

namespace BuildManager.Models
{
    public class RefreshToken
    {
        [Key]
        public int RefreshTokenId { get; set; }

        public int CompanyUserId { get; set; }

        [Required]
        public string Token { get; set; } = string.Empty;

        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool     IsRevoked { get; set; } = false;

        public CompanyUser CompanyUser { get; set; } = null!;
    }
}
