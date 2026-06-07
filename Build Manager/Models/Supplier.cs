namespace BuildManager.Models
{
    /// <summary>
    /// Represents a material supplier.
    /// Corresponds to the "Supplier Details" screen.
    /// </summary>
    public class Supplier
    {
        public int SupplierId { get; set; }

        public string SupplierName { get; set; } = string.Empty;

        // Address
        public string? DoorNoAndStreetName { get; set; }
        public string? AreaName { get; set; }
        public string? Place { get; set; }
        public string? PinCode { get; set; }
        public string? CityOrTalukName { get; set; }
        public string? DistrictAndStateName { get; set; }

        // Contact
        public string? PhoneNumber { get; set; }
        public string? MobileNumber { get; set; }
        public string? FaxNumber { get; set; }
        public string? EmailId { get; set; }

        // Bank / Financial details
        public string? WebsiteName { get; set; }
        public string? AccountNumber { get; set; }
        public string? AccountName { get; set; }
        public string? AccountType { get; set; }
        public string? BankName { get; set; }
        public string? BankBranch { get; set; }
        public string? BranchCode { get; set; }
        public string? IfscCode { get; set; }
        public string? PanCardNumber { get; set; }
        public string? TinNumber { get; set; }
        public string? CstNumber { get; set; }
        public string? AadhaarNumber { get; set; }

        // Navigation property
        public ICollection<SupplierTransaction> SupplierTransactions { get; set; } = new List<SupplierTransaction>();
    }
}
