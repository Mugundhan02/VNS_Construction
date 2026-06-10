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

        public Address? Address { get; set; }
        public ContactInfo? ContactInfo { get; set; }
        public BankDetails? BankDetails { get; set; }
        public IdentityDetails? IdentityDetails { get; set; }
        public EstimateDetails? EstimateDetails { get; set; }

        // Simplified collection initializers to fix IDE0028 style messages
        public ICollection<ClientTransaction> ClientTransactions { get; set; } = [];
        public ICollection<SupplierTransaction> SupplierTransactions { get; set; } = [];
        public ICollection<SubContractorTransaction> SubContractorTransactions { get; set; } = [];
        public ICollection<CompanyExpenseTransaction> CompanyExpenseTransactions { get; set; } = [];
    }
}