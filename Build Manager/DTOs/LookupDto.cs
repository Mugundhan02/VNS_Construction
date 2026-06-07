using System.ComponentModel.DataAnnotations;

namespace BuildManager.DTOs
{
    // ── Payment Type ──────────────────────────────────────────────────────────

    public class PaymentTypeRequestDto
    {
        [Required, MaxLength(100)]
        public string PaymentTypeName { get; set; } = string.Empty;
    }

    public class PaymentTypeResponseDto
    {
        public int    PaymentTypeId   { get; set; }
        public string PaymentTypeName { get; set; } = string.Empty;
    }

    // ── Whom ──────────────────────────────────────────────────────────────────

    public class WhomRequestDto
    {
        [Required, MaxLength(200)]
        public string WhomName { get; set; } = string.Empty;
    }

    public class WhomResponseDto
    {
        public int    WhomId   { get; set; }
        public string WhomName { get; set; } = string.Empty;
    }

    // ── Office Expense ────────────────────────────────────────────────────────

    public class OfficeExpenseRequestDto
    {
        [Required, MaxLength(200)]
        public string ExpenseName { get; set; } = string.Empty;
    }

    public class OfficeExpenseResponseDto
    {
        public int    OfficeExpenseId { get; set; }
        public string ExpenseName     { get; set; } = string.Empty;
    }

    // ── Company Bank ──────────────────────────────────────────────────────────

    public class CompanyBankRequestDto
    {
        [Required]
        public int CompanyId { get; set; }

        [Required, MaxLength(200)]
        public string BankName { get; set; } = string.Empty;

        public string? BankBranch    { get; set; }
        public string? BranchCode    { get; set; }

        [MaxLength(20)]
        public string? IfscCode      { get; set; }
        public string? AccountName   { get; set; }
        public string? AccountNumber { get; set; }
        public string? AccountType   { get; set; }
    }

    public class CompanyBankResponseDto
    {
        public int    CompanyBankId  { get; set; }
        public int    CompanyId      { get; set; }
        public string BankName       { get; set; } = string.Empty;
        public string? BankBranch    { get; set; }

        // IFSC shown for reference — account number intentionally omitted
        public string? IfscCode      { get; set; }
        public string? AccountName   { get; set; }
        public string? AccountType   { get; set; }
    }

    // ── Installment Term ──────────────────────────────────────────────────────

    public class InstallmentTermRequestDto
    {
        [Required, MaxLength(200)]
        public string TermName { get; set; } = string.Empty;

        public int?    NumberOfInstallments { get; set; }
        public string? Description          { get; set; }
    }

    public class InstallmentTermResponseDto
    {
        public int    InstallmentTermId     { get; set; }
        public string TermName              { get; set; } = string.Empty;
        public int?   NumberOfInstallments  { get; set; }
        public string? Description          { get; set; }
    }
}
