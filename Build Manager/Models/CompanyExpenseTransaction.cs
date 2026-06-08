namespace BuildManager.Models
{
    /// <summary>
    /// Represents a company office expense transaction.
    /// Corresponds to the "Company Expense Details" transaction screen.
    /// </summary>
    public class CompanyExpenseTransaction
    {
        public int CompanyExpenseTransactionId { get; set; }

        public DateTime TransactionDate { get; set; }

        public int  CompanyId       { get; set; }
        public int  OfficeExpenseId { get; set; }
        public int? ClientId        { get; set; }

        public string?  MaterialOrJobWorkName { get; set; }
        public decimal  Amount            { get; set; } = 0;
        public decimal  ReceivedAmount    { get; set; } = 0;
        public decimal  PaidAmount        { get; set; } = 0;
        public int?     PaymentTypeId     { get; set; }
        public int?     ToWhomId          { get; set; }
        public string?  TransactionType   { get; set; }
        public string?  Remarks           { get; set; }

        // Navigation properties
        public Company       Company       { get; set; } = null!;
        public OfficeExpense OfficeExpense { get; set; } = null!;
        public Client?       Client        { get; set; }
        public PaymentType?  PaymentType   { get; set; }
        public Whom?         ToWhom        { get; set; }
    }
}
