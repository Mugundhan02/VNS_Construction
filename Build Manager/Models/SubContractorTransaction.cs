namespace BuildManager.Models
{
    /// <summary>
    /// Records a labour/job work transaction with a sub-contractor for a client/site.
    /// Corresponds to the "SubContractor Transaction Details" screen.
    /// Shows TxnDate, ClientName, SubContractorName, JobWorkName, Qty, Unit, Rate, Amount, PaidAmount, PaymentType, ToWhom.
    /// </summary>
    public class SubContractorTransaction
    {
        public int SubContractorTransactionId { get; set; }

        public DateTime TransactionDate { get; set; }

        public int ClientId { get; set; }

        public int SubContractorId { get; set; }

        public int JobWorkId { get; set; }

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
        public SubContractor SubContractor { get; set; } = null!;
        public JobWork JobWork { get; set; } = null!;
        public PaymentType? PaymentType { get; set; }
        public Whom? ToWhom { get; set; }
    }
}
