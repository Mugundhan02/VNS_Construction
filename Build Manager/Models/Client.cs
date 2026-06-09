using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BuildManager.Models
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }

        [Required, MaxLength(200)]
        public string ClientName { get; set; } = string.Empty;

        public Address?         Address         { get; set; }
        public ContactInfo?     ContactInfo     { get; set; }
        public BankDetails?     BankDetails     { get; set; }
        public IdentityDetails? IdentityDetails { get; set; }
        public EstimateDetails? EstimateDetails { get; set; }

        public ICollection<ClientTransaction>        ClientTransactions        { get; set; } = new List<ClientTransaction>();
        public ICollection<SupplierTransaction>      SupplierTransactions      { get; set; } = new List<SupplierTransaction>();
        public ICollection<SubContractorTransaction> SubContractorTransactions { get; set; } = new List<SubContractorTransaction>();
        public ICollection<CompanyExpenseTransaction> CompanyExpenseTransactions { get; set; } = new List<CompanyExpenseTransaction>();
    }
}
