namespace BuildManager.Models
{
    /// <summary>
    /// Represents a construction material in the master list.
    /// Corresponds to the "Company Material Details" screen.
    /// Example values: Cement, 1.5 Jally, Flyash brick, M.Sand, etc.
    /// </summary>
    public class Material
    {
        public int MaterialId { get; set; }

        public string MaterialName { get; set; } = string.Empty;

        /// <summary>
        /// Unit of measurement (e.g., Bag, Nos, m3, Unit, Kg, Load)
        /// </summary>
        public string? Unit { get; set; }

        /// <summary>
        /// Default/standard rate per unit
        /// </summary>
        public decimal Rate { get; set; } = 0;

        // Navigation property
        public ICollection<SupplierTransaction> SupplierTransactions { get; set; } = new List<SupplierTransaction>();
    }
}
