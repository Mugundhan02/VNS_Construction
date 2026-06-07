namespace BuildManager.Models
{
    /// <summary>
    /// Records a company office expense transaction.
    /// Corresponds to the "Company Expense Transaction Details" screen.
    /// Shows TxnDate, ClientName, TransactionType, ExpenseName, MaterialOrJobWork, ReceivedAmount, PaidAmount, Amount, WhomName, PaymentType.
    /// </summary>
    public class CompanyExpenseTransaction
    {
        public int CompanyExpenseTransactionId { get; set; }

        public DateTime TransactionDate { get; set; }

        public int? ClientId { get; set; }

        public int CompanyId { get; set; }

        public int OfficeExpenseId { get; set; }

        public string? MaterialOrJobWorkName { get; set; }

        public decimal Amount { get; set; } = 0;

        public decimal ReceivedAmount { get; set; } = 0;

        public decimal PaidAmount { get; set; } = 0;

        public int? PaymentTypeId { get; set; }

        public int? ToWhomId { get; set; }

        public string? TransactionType { get; set; }

        public string? Remarks { get; set; }

        // Navigation properties
        public Client? Client { get; set; }
        public Company Company { get; set; } = null!;
        public OfficeExpense OfficeExpense { get; set; } = null!;
        public PaymentType? PaymentType { get; set; }
        public Whom? ToWhom { get; set; }
    }
}
