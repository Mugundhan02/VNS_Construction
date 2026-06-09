using System.ComponentModel.DataAnnotations;

namespace BuildManager.Models
{
    public class CompanyBank
    {
        [Key]
        public int CompanyBankId { get; set; }

        public int CompanyId { get; set; }

        [Required, MaxLength(200)]
        public string BankName { get; set; } = string.Empty;

        [MaxLength(100)] public string? BankBranch    { get; set; }
        [MaxLength(20)]  public string? BranchCode    { get; set; }
        [MaxLength(11)]  public string? IfscCode      { get; set; }
        [MaxLength(100)] public string? AccountName   { get; set; }
        [MaxLength(30)]  public string? AccountNumber { get; set; }
        [MaxLength(30)]  public string? AccountType   { get; set; }

        public Company Company { get; set; } = null!;
    }
}
