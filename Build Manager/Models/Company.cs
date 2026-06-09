using System.ComponentModel.DataAnnotations;

namespace BuildManager.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }

        [Required, MaxLength(200)]
        public string CompanyName { get; set; } = string.Empty;

        public Address?         Address         { get; set; }
        public ContactInfo?     ContactInfo     { get; set; }
        public IdentityDetails? IdentityDetails { get; set; }

        public ICollection<CompanyUser> CompanyUsers { get; set; } = new List<CompanyUser>();
        public ICollection<CompanyBank> CompanyBanks { get; set; } = new List<CompanyBank>();
    }
}
