using System.ComponentModel.DataAnnotations;

namespace BuildManager.DTOs
{
    // ── Payment Type ──────────────────────────────────────────────────────────

    public class PaymentTypeRequestDto
    {
        [Required]
        [MaxLength(100)]
        public string PaymentTypeName { get; set; } = string.Empty;
    }

    public class PaymentTypeUpdateDto
    {
        [Required]
        [MaxLength(100)]
        public string PaymentTypeName { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }

    public class PaymentTypeResponseDto
    {
        public int PaymentTypeId { get; set; }
        public string PaymentTypeName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    // ── Whom ──────────────────────────────────────────────────────────────────

    public class WhomRequestDto
    {
        [Required]
        [MaxLength(200)]
        public string WhomName { get; set; } = string.Empty;
    }

    public class WhomUpdateDto
    {
        [Required]
        [MaxLength(200)]
        public string WhomName { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }

    public class WhomResponseDto
    {
        public int WhomId { get; set; }
        public string WhomName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    // ── Office Expense ────────────────────────────────────────────────────────

    public class OfficeExpenseRequestDto
    {
        [Required]
        [MaxLength(200)]
        public string ExpenseName { get; set; } = string.Empty;
    }

    public class OfficeExpenseUpdateDto
    {
        [Required]
        [MaxLength(200)]
        public string ExpenseName { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }

    public class OfficeExpenseResponseDto
    {
        public int OfficeExpenseId { get; set; }
        public string ExpenseName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    // ── Company Bank ──────────────────────────────────────────────────────────

    public class CompanyBankRequestDto
    {
        [Required]
        public int CompanyId { get; set; }

        [Required]
        [MaxLength(200)]
        public string BankName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? BankBranch { get; set; }

        [MaxLength(20)]
        public string? BranchCode { get; set; }

        [MaxLength(11)]
        [RegularExpression(@"^[A-Z]{4}0[A-Z0-9]{6}$", ErrorMessage = "Invalid IFSC code format.")]
        public string? IfscCode { get; set; }

        [MaxLength(100)]
        public string? AccountName { get; set; }

        [MaxLength(30)]
        public string? AccountNumber { get; set; }

        [MaxLength(30)]
        public string? AccountType { get; set; }
    }

    public class CompanyBankUpdateDto
    {
        [Required]
        [MaxLength(200)]
        public string BankName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? BankBranch { get; set; }

        [MaxLength(20)]
        public string? BranchCode { get; set; }

        [MaxLength(11)]
        [RegularExpression(@"^[A-Z]{4}0[A-Z0-9]{6}$", ErrorMessage = "Invalid IFSC code format.")]
        public string? IfscCode { get; set; }

        [MaxLength(100)]
        public string? AccountName { get; set; }

        [MaxLength(30)]
        public string? AccountNumber { get; set; }

        [MaxLength(30)]
        public string? AccountType { get; set; }

        public bool IsActive { get; set; }
    }

    public class CompanyBankResponseDto
    {
        public int CompanyBankId { get; set; }
        public int CompanyId { get; set; }
        public string BankName { get; set; } = string.Empty;
        public string? BankBranch { get; set; }
        public string? IfscCode { get; set; }
        public string? AccountName { get; set; }
        public string? AccountType { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    // ── Installment Term ──────────────────────────────────────────────────────

    public class InstallmentTermRequestDto
    {
        [Required]
        [MaxLength(200)]
        public string TermName { get; set; } = string.Empty;

        [Range(1, 120, ErrorMessage = "Number of installments must be between 1 and 120.")]
        public int? NumberOfInstallments { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }
    }

    public class InstallmentTermUpdateDto
    {
        [Required]
        [MaxLength(200)]
        public string TermName { get; set; } = string.Empty;

        [Range(1, 120, ErrorMessage = "Number of installments must be between 1 and 120.")]
        public int? NumberOfInstallments { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public bool IsActive { get; set; }
    }

    public class InstallmentTermResponseDto
    {
        public int InstallmentTermId { get; set; }
        public string TermName { get; set; } = string.Empty;
        public int? NumberOfInstallments { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}