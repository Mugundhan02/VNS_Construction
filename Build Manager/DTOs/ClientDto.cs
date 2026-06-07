using System.ComponentModel.DataAnnotations;

namespace BuildManager.DTOs
{
    public class ClientRequestDto
    {
        [Required, MaxLength(200)]
        public string ClientName { get; set; } = string.Empty;

        // Address
        public string? DoorNoAndStreetName  { get; set; }
        public string? AreaName             { get; set; }
        public string? Place                { get; set; }

        [MaxLength(10)]
        public string? PinCode              { get; set; }
        public string? CityOrTalukName      { get; set; }
        public string? DistrictAndStateName { get; set; }

        // Contact
        [MaxLength(20)] public string? PhoneNumber  { get; set; }
        [MaxLength(20)] public string? MobileNumber { get; set; }
        [MaxLength(20)] public string? FaxNumber    { get; set; }

        [EmailAddress, MaxLength(200)]
        public string? EmailId     { get; set; }
        [MaxLength(200)]
        public string? WebsiteName { get; set; }

        // Bank
        public string? AccountNumber { get; set; }
        public string? AccountName   { get; set; }
        public string? AccountType   { get; set; }
        public string? BankName      { get; set; }
        public string? BankBranch    { get; set; }
        public string? BranchCode    { get; set; }
        public string? IfscCode      { get; set; }

        // Tax
        [MaxLength(20)] public string? PanCardNumber { get; set; }
        [MaxLength(30)] public string? TinNumber     { get; set; }
        [MaxLength(30)] public string? CstNumber     { get; set; }
        [MaxLength(20)] public string? AadhaarNumber { get; set; }

        // Estimate
        public decimal? EstimateUnit   { get; set; }
        public decimal? EstimateRate   { get; set; }
        public decimal? EstimateAmount { get; set; }
    }

    public class ClientResponseDto
    {
        public int    ClientId              { get; set; }
        public string ClientName            { get; set; } = string.Empty;

        // Address — always shown
        public string? Place                { get; set; }
        public string? AreaName             { get; set; }
        public string? CityOrTalukName      { get; set; }
        public string? DistrictAndStateName { get; set; }
        public string? PinCode              { get; set; }

        // Contact — always shown
        public string? MobileNumber { get; set; }
        public string? PhoneNumber  { get; set; }
        public string? EmailId      { get; set; }

        // Bank — shown (account number masked at service layer if needed later)
        public string? BankName    { get; set; }
        public string? BankBranch  { get; set; }
        public string? AccountType { get; set; }
        public string? AccountName { get; set; }
        public string? IfscCode    { get; set; }

        // Estimate — shown for project tracking
        public decimal? EstimateUnit   { get; set; }
        public decimal? EstimateRate   { get; set; }
        public decimal? EstimateAmount { get; set; }
    }
}
