using System.ComponentModel.DataAnnotations;

namespace BuildManager.Models
{
    public class CompanyExpenseTransaction
    {
        [Key]
        public int CompanyExpenseTransactionId { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        public int  CompanyId       { get; set; }
        public int  OfficeExpenseId { get; set; }
        public int? ClientId        { get; set; }

        public string? MaterialOrJobWorkName { get; set; }
        public string? TransactionType       { get; set; }
        public string? Remarks               { get; set; }

        [Range(0, double.MaxValue)] public decimal Amount         { get; set; } = 0;
        [Range(0, double.MaxValue)] public decimal ReceivedAmount { get; set; } = 0;
        [Range(0, double.MaxValue)] public decimal PaidAmount     { get; set; } = 0;

        public int? PaymentTypeId { get; set; }
        public int? ToWhomId      { get; set; }

        public Company       Company       { get; set; } = null!;
        public OfficeExpense OfficeExpense { get; set; } = null!;
        public Client?       Client        { get; set; }
        public PaymentType?  PaymentType   { get; set; }
        public Whom?         ToWhom        { get; set; }
    }
}
