using System.ComponentModel.DataAnnotations;

namespace BuildManager.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }

        [Required, MaxLength(200)]
        public string SupplierName { get; set; } = string.Empty;

        public Address?         Address         { get; set; }
        public ContactInfo?     ContactInfo     { get; set; }
        public BankDetails?     BankDetails     { get; set; }
        public IdentityDetails? IdentityDetails { get; set; }

        public ICollection<SupplierTransaction> SupplierTransactions { get; set; } = new List<SupplierTransaction>();
    }
}
