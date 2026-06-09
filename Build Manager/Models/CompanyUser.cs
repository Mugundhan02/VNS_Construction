using System.ComponentModel.DataAnnotations;

namespace BuildManager.Models
{
    public class CompanyUser
    {
        [Key]
        public int CompanyUserId { get; set; }

        public int CompanyId { get; set; }

        [Required, MaxLength(100)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public string PasswordSalt { get; set; } = string.Empty;

        [Required, MaxLength(20)]
        public string UserType { get; set; } = "User";

        public bool IsActive { get; set; } = true;

        public Company                   Company       { get; set; } = null!;
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
