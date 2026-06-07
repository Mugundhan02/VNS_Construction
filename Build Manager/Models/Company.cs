namespace BuildManager.Models
{
    /// <summary>
    /// Represents the company (VNS Construction) settings and details.
    /// Corresponds to the "Company Settings Details" screen.
    /// </summary>
    public class Company
    {
        public int CompanyId { get; set; }

        public string CompanyName { get; set; } = string.Empty;

        public string? DoorNoAndStreetName { get; set; }

        public string? AreaName { get; set; }

        public string? Place { get; set; }

        public string? PinCode { get; set; }

        public string? CityOrTalukName { get; set; }

        public string? DistrictAndStateName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? MobileNumber { get; set; }

        public string? FaxNumber { get; set; }

        public string? EmailId { get; set; }

        public string? WebsiteName { get; set; }

        public string? PanCardNumber { get; set; }

        public string? TinNumber { get; set; }

        public string? CstNumber { get; set; }

        // Navigation properties
        public ICollection<CompanyUser> CompanyUsers { get; set; } = new List<CompanyUser>();
        public ICollection<CompanyBank> CompanyBanks { get; set; } = new List<CompanyBank>();
    }
}
