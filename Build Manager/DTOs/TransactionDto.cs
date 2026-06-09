using System.ComponentModel.DataAnnotations;

namespace BuildManager.DTOs
{
    // ── Client Transaction ────────────────────────────────────────────────────

    public class ClientTransactionRequestDto
    {
        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Credit amount must be a positive value.")]
        public decimal CreditAmount { get; set; } = 0;

        [Range(0, double.MaxValue, ErrorMessage = "Debit amount must be a positive value.")]
        public decimal DebitAmount { get; set; } = 0;

        public int? PaymentTypeId { get; set; }

        public int? ByWhomId { get; set; }

        [MaxLength(500)]
        public string? Remarks { get; set; }
    }

    public class ClientTransactionUpdateDto
    {
        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Credit amount must be a positive value.")]
        public decimal CreditAmount { get; set; } = 0;

        [Range(0, double.MaxValue, ErrorMessage = "Debit amount must be a positive value.")]
        public decimal DebitAmount { get; set; } = 0;

        public int? PaymentTypeId { get; set; }

        public int? ByWhomId { get; set; }

        [MaxLength(500)]
        public string? Remarks { get; set; }
    }

    public class ClientTransactionResponseDto
    {
        public int ClientTransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public decimal CreditAmount { get; set; }
        public decimal DebitAmount { get; set; }
        public string? PaymentTypeName { get; set; }
        public string? ByWhomName { get; set; }
        public string? Remarks { get; set; }
    }

    // ── Supplier Transaction ──────────────────────────────────────────────────

    public class SupplierTransactionRequestDto
    {
        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public int SupplierId { get; set; }

        [Required]
        public int MaterialId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Quantity must be a positive value.")]
        public decimal Quantity { get; set; } = 0;

        [MaxLength(50)]
        public string? Unit { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Rate must be a positive value.")]
        public decimal Rate { get; set; } = 0;

        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a positive value.")]
        public decimal Amount { get; set; } = 0;

        [Range(0, double.MaxValue, ErrorMessage = "Paid amount must be a positive value.")]
        public decimal PaidAmount { get; set; } = 0;

        public int? PaymentTypeId { get; set; }

        public int? ToWhomId { get; set; }

        [MaxLength(500)]
        public string? Remarks { get; set; }

        public bool IsSubBill { get; set; } = false;
    }

    public class SupplierTransactionUpdateDto
    {
        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public int SupplierId { get; set; }

        [Required]
        public int MaterialId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Quantity must be a positive value.")]
        public decimal Quantity { get; set; } = 0;

        [MaxLength(50)]
        public string? Unit { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Rate must be a positive value.")]
        public decimal Rate { get; set; } = 0;

        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a positive value.")]
        public decimal Amount { get; set; } = 0;

        [Range(0, double.MaxValue, ErrorMessage = "Paid amount must be a positive value.")]
        public decimal PaidAmount { get; set; } = 0;

        public int? PaymentTypeId { get; set; }

        public int? ToWhomId { get; set; }

        [MaxLength(500)]
        public string? Remarks { get; set; }

        public bool IsSubBill { get; set; } = false;
    }

    public class SupplierTransactionResponseDto
    {
        public int SupplierTransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string SupplierName { get; set; } = string.Empty;
        public string MaterialName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string? Unit { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal BalanceAmount => Amount - PaidAmount;
        public string? PaymentTypeName { get; set; }
        public string? ToWhomName { get; set; }
        public string? Remarks { get; set; }
    }

    // ── SubContractor Transaction ─────────────────────────────────────────────

    public class SubContractorTransactionRequestDto
    {
        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public int SubContractorId { get; set; }

        [Required]
        public int JobWorkId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Quantity must be a positive value.")]
        public decimal Quantity { get; set; } = 0;

        [MaxLength(50)]
        public string? Unit { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Rate must be a positive value.")]
        public decimal Rate { get; set; } = 0;

        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a positive value.")]
        public decimal Amount { get; set; } = 0;

        [Range(0, double.MaxValue, ErrorMessage = "Paid amount must be a positive value.")]
        public decimal PaidAmount { get; set; } = 0;

        public int? PaymentTypeId { get; set; }

        public int? ToWhomId { get; set; }

        [MaxLength(500)]
        public string? Remarks { get; set; }

        public bool IsSubBill { get; set; } = false;
    }

    public class SubContractorTransactionUpdateDto
    {
        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public int SubContractorId { get; set; }

        [Required]
        public int JobWorkId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Quantity must be a positive value.")]
        public decimal Quantity { get; set; } = 0;

        [MaxLength(50)]
        public string? Unit { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Rate must be a positive value.")]
        public decimal Rate { get; set; } = 0;

        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a positive value.")]
        public decimal Amount { get; set; } = 0;

        [Range(0, double.MaxValue, ErrorMessage = "Paid amount must be a positive value.")]
        public decimal PaidAmount { get; set; } = 0;

        public int? PaymentTypeId { get; set; }

        public int? ToWhomId { get; set; }

        [MaxLength(500)]
        public string? Remarks { get; set; }

        public bool IsSubBill { get; set; } = false;
    }

    public class SubContractorTransactionResponseDto
    {
        public int SubContractorTransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string SubContractorName { get; set; } = string.Empty;
        public string JobWorkName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string? Unit { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal BalanceAmount => Amount - PaidAmount;
        public string? PaymentTypeName { get; set; }
        public string? ToWhomName { get; set; }
        public string? Remarks { get; set; }
    }

    // ── Company Expense Transaction ───────────────────────────────────────────

    public class CompanyExpenseTransactionRequestDto
    {
        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [Required]
        public int OfficeExpenseId { get; set; }

        public int? ClientId { get; set; }

        [MaxLength(200)]
        public string? MaterialOrJobWorkName { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a positive value.")]
        public decimal Amount { get; set; } = 0;

        [Range(0, double.MaxValue, ErrorMessage = "Received amount must be a positive value.")]
        public decimal ReceivedAmount { get; set; } = 0;

        [Range(0, double.MaxValue, ErrorMessage = "Paid amount must be a positive value.")]
        public decimal PaidAmount { get; set; } = 0;

        public int? PaymentTypeId { get; set; }

        public int? ToWhomId { get; set; }

        [MaxLength(50)]
        public string? TransactionType { get; set; }

        [MaxLength(500)]
        public string? Remarks { get; set; }
    }

    public class CompanyExpenseTransactionUpdateDto
    {
        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [Required]
        public int OfficeExpenseId { get; set; }

        public int? ClientId { get; set; }

        [MaxLength(200)]
        public string? MaterialOrJobWorkName { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a positive value.")]
        public decimal Amount { get; set; } = 0;

        [Range(0, double.MaxValue, ErrorMessage = "Received amount must be a positive value.")]
        public decimal ReceivedAmount { get; set; } = 0;

        [Range(0, double.MaxValue, ErrorMessage = "Paid amount must be a positive value.")]
        public decimal PaidAmount { get; set; } = 0;

        public int? PaymentTypeId { get; set; }

        public int? ToWhomId { get; set; }

        [MaxLength(50)]
        public string? TransactionType { get; set; }

        [MaxLength(500)]
        public string? Remarks { get; set; }
    }

    public class CompanyExpenseTransactionResponseDto
    {
        public int CompanyExpenseTransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string? ClientName { get; set; }
        public string ExpenseName { get; set; } = string.Empty;
        public string? MaterialOrJobWorkName { get; set; }
        public decimal Amount { get; set; }
        public decimal ReceivedAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal BalanceAmount => Amount - PaidAmount;
        public string? PaymentTypeName { get; set; }
        public string? ToWhomName { get; set; }
        public string? TransactionType { get; set; }
        public string? Remarks { get; set; }
    }

    // ── Summary / Dashboard ───────────────────────────────────────────────────

    public class ClientSummaryDto
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public decimal CreditsAmount { get; set; }
        public decimal DebitsAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public decimal EstimateUnits { get; set; }
        public decimal EstimateRate { get; set; }
        public decimal EstimateAmount { get; set; }
        public decimal EstimateAmountReceived { get; set; }
        public decimal EstimateAmountExpenses { get; set; }
    }

    public class SupplierSummaryDto
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; } = string.Empty;
        public decimal PayableAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal BalanceAmount { get; set; }
    }

    public class SubContractorSummaryDto
    {
        public int SubContractorId { get; set; }
        public string SubContractorName { get; set; } = string.Empty;
        public decimal PayableAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal BalanceAmount { get; set; }
    }

    public class CompanySummaryDto
    {
        public string CompanyName { get; set; } = string.Empty;
        public decimal CreditsAmount { get; set; }
        public decimal DebitsAmount { get; set; }
        public decimal BalanceAmount { get; set; }
    }
}