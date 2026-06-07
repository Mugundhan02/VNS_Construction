namespace BuildManager.Models
{
    /// <summary>
    /// Represents installment / term definitions used in transactions.
    /// Accessible from both Masters and Transactions menus.
    /// </summary>
    public class InstallmentTerm
    {
        public int InstallmentTermId { get; set; }

        public string TermName { get; set; } = string.Empty;

        public int? NumberOfInstallments { get; set; }

        public string? Description { get; set; }
    }
}
