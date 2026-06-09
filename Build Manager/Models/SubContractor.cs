using System.ComponentModel.DataAnnotations;

namespace BuildManager.Models
{
    public class SubContractor
    {
        [Key]
        public int SubContractorId { get; set; }

        [Required, MaxLength(200)]
        public string SubContractorName { get; set; } = string.Empty;

        public Address?         Address         { get; set; }
        public ContactInfo?     ContactInfo     { get; set; }
        public BankDetails?     BankDetails     { get; set; }
        public IdentityDetails? IdentityDetails { get; set; }
        public WorkDetails?     WorkDetails     { get; set; }

        public ICollection<SubContractorTransaction> SubContractorTransactions { get; set; } = new List<SubContractorTransaction>();
    }
}
