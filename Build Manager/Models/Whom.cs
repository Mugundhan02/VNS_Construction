namespace BuildManager.Models
{
    /// <summary>
    /// Lookup table for "To Whom" references used in transactions.
    /// Corresponds to the "Company Whom Details" screen.
    /// Example value: ADAIKKAN.
    /// </summary>
    public class Whom
    {
        public int WhomId { get; set; }

        public string WhomName { get; set; } = string.Empty;
    }
}
