namespace BuildManager.Models
{
    /// <summary>
    /// Represents the bank account details of the company.
    /// Corresponds to the "Company Bank Details" screen.
    /// </summary>
    public class CompanyBank
    {
        public int CompanyBankId { get; set; }

        public int CompanyId { get; set; }

        public string BankName { get; set; } = string.Empty;

        public string? BankBranch { get; set; }

        public string? BranchCode { get; set; }

        public string? IfscCode { get; set; }

        public string? AccountName { get; set; }

        public string? AccountNumber { get; set; }

        /// <summary>
        /// e.g., Savings, Current
        /// </summary>
        public string? AccountType { get; set; }

        // Navigation property
        public Company Company { get; set; } = null!;
    }
}
