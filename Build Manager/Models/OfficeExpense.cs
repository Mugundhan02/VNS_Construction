using System.ComponentModel.DataAnnotations;

namespace BuildManager.Models
{
    public class OfficeExpense
    {
        [Key]
        public int OfficeExpenseId { get; set; }

        [Required, MaxLength(200)]
        public string ExpenseName { get; set; } = string.Empty;

        public ICollection<CompanyExpenseTransaction> CompanyExpenseTransactions { get; set; } = new List<CompanyExpenseTransaction>();
    }
}
