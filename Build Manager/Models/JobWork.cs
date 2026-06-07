namespace BuildManager.Models
{
    /// <summary>
    /// Represents a job work / labour work item in the master list.
    /// Corresponds to the "Company JobWork Details" screen.
    /// Examples: Site Cleaning, Earthwork Excavations, Column Shutter, Footing Concrete, etc.
    /// </summary>
    public class JobWork
    {
        public int JobWorkId { get; set; }

        public string JobWorkName { get; set; } = string.Empty;

        /// <summary>
        /// Unit of measurement (e.g., Cft, Sft, Rft, Kg, Nos, Lit)
        /// </summary>
        public string? Unit { get; set; }

        /// <summary>
        /// Standard rate per unit
        /// </summary>
        public decimal Rate { get; set; } = 0;

        // Navigation property
        public ICollection<SubContractorTransaction> SubContractorTransactions { get; set; } = new List<SubContractorTransaction>();
    }
}
