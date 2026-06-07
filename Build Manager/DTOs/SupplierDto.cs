using System.ComponentModel.DataAnnotations;

namespace BuildManager.DTOs
{
    public class SupplierRequestDto
    {
        [Required, MaxLength(200)]
        public string SupplierName { get; set; } = string.Empty;

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
    }

    public class SupplierResponseDto
    {
        public int    SupplierId   { get; set; }
        public string SupplierName { get; set; } = string.Empty;

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

        // Bank — name/branch for payment reference (no raw account number)
        public string? BankName    { get; set; }
        public string? BankBranch  { get; set; }
        public string? AccountType { get; set; }
        public string? AccountName { get; set; }
        public string? IfscCode    { get; set; }
    }
}
