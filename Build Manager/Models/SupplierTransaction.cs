namespace BuildManager.Models
{
    /// <summary>
    /// Records a material purchase from a supplier for a specific client/site.
    /// Corresponds to the "Supplier Transaction Details" screen.
    /// Shows TxnDate, ClientName, SupplierName, MaterialName, Qty, Unit, Rate, Amount, PaidAmount, PaymentType, ToWhom.
    /// </summary>
    public class SupplierTransaction
    {
        public int SupplierTransactionId { get; set; }

        public DateTime TransactionDate { get; set; }

        public int ClientId { get; set; }

        public int SupplierId { get; set; }

        public int MaterialId { get; set; }

        public decimal Quantity { get; set; } = 0;

        public string? Unit { get; set; }

        public decimal Rate { get; set; } = 0;

        public decimal Amount { get; set; } = 0;

        public decimal PaidAmount { get; set; } = 0;

        public int? PaymentTypeId { get; set; }

        public int? ToWhomId { get; set; }

        public string? Remarks { get; set; }

        /// <summary>
        /// Indicates if this entry is a sub-bill reference
        /// </summary>
        public bool IsSubBill { get; set; } = false;

        // Navigation properties
        public Client Client { get; set; } = null!;
        public Supplier Supplier { get; set; } = null!;
        public Material Material { get; set; } = null!;
        public PaymentType? PaymentType { get; set; }
        public Whom? ToWhom { get; set; }
    }
}
