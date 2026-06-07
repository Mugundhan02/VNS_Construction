namespace BuildManager.Models
{
    /// <summary>
    /// Lookup table for types of office expenses.
    /// Corresponds to the "Company Expense Details" screen.
    /// Example values: EB Bill, Food Exp., Mobile Bill, Office Rent, Salary, etc.
    /// </summary>
    public class OfficeExpense
    {
        public int OfficeExpenseId { get; set; }

        public string ExpenseName { get; set; } = string.Empty;

        // Navigation property
        public ICollection<CompanyExpenseTransaction> CompanyExpenseTransactions { get; set; } = new List<CompanyExpenseTransaction>();
    }
}
