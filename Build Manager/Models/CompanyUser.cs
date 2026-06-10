using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildManager.Models
{
    public class CompanyUser
    {
        [Key]
        public int CompanyUserId { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public Company? Company { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserName { get; set; } = string.Empty;

        // ── 👈 ADD THIS COLUMN TO YOUR CLASS ──
        [Required]
        [MaxLength(250)]
        public string EmailId { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public string PasswordSalt { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string UserType { get; set; } = "User";

        public bool IsActive { get; set; } = true;
    }
}