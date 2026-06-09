using System.ComponentModel.DataAnnotations;

namespace BuildManager.Models
{
    public class SupplierTransaction
    {
        [Key]
        public int SupplierTransactionId { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        public int ClientId   { get; set; }
        public int SupplierId { get; set; }
        public int MaterialId { get; set; }

        [Range(0, double.MaxValue)] public decimal Quantity   { get; set; } = 0;
        [MaxLength(50)]             public string? Unit       { get; set; }
        [Range(0, double.MaxValue)] public decimal Rate       { get; set; } = 0;
        [Range(0, double.MaxValue)] public decimal Amount     { get; set; } = 0;
        [Range(0, double.MaxValue)] public decimal PaidAmount { get; set; } = 0;

        public int?    PaymentTypeId { get; set; }
        public int?    ToWhomId      { get; set; }
        public string? Remarks       { get; set; }
        public bool    IsSubBill     { get; set; } = false;

        public Client       Client      { get; set; } = null!;
        public Supplier     Supplier    { get; set; } = null!;
        public Material     Material    { get; set; } = null!;
        public PaymentType? PaymentType { get; set; }
        public Whom?        ToWhom      { get; set; }
    }
}
