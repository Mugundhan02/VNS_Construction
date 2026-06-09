using System.ComponentModel.DataAnnotations;

namespace BuildManager.DTOs
{
    public class SubContractorRequestDto
    {
        [Required]
        [MaxLength(200)]
        public string SubContractorName { get; set; } = string.Empty;

        // Address
        [MaxLength(150)]
        public string? DoorNoAndStreetName { get; set; }

        [MaxLength(100)]
        public string? AreaName { get; set; }

        [MaxLength(100)]
        public string? Place { get; set; }

        [MaxLength(10)]
        public string? PinCode { get; set; }

        [MaxLength(100)]
        public string? CityOrTalukName { get; set; }

        [MaxLength(150)]
        public string? DistrictAndStateName { get; set; }

        // Contact
        [Phone]
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [Phone]
        [MaxLength(20)]
        public string? MobileNumber { get; set; }

        [MaxLength(20)]
        public string? FaxNumber { get; set; }

        [EmailAddress]
        [MaxLength(200)]
        public string? EmailId { get; set; }

        [Url]
        [MaxLength(200)]
        public string? WebsiteName { get; set; }

        // Bank
        [MaxLength(30)]
        public string? AccountNumber { get; set; }

        [MaxLength(100)]
        public string? AccountName { get; set; }

        [MaxLength(30)]
        public string? AccountType { get; set; }

        [MaxLength(100)]
        public string? BankName { get; set; }

        [MaxLength(100)]
        public string? BankBranch { get; set; }

        [MaxLength(20)]
        public string? BranchCode { get; set; }

        [MaxLength(11)]
        [RegularExpression(@"^[A-Z]{4}0[A-Z0-9]{6}$", ErrorMessage = "Invalid IFSC code format.")]
        public string? IfscCode { get; set; }

        // Tax
        [MaxLength(10)]
        [RegularExpression(@"^[A-Z]{5}[0-9]{4}[A-Z]{1}$", ErrorMessage = "Invalid PAN card format.")]
        public string? PanCardNumber { get; set; }

        [MaxLength(30)]
        public string? TinNumber { get; set; }

        [MaxLength(12)]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "Aadhaar number must be exactly 12 digits.")]
        public string? AadhaarNumber { get; set; }

        // Work
        [MaxLength(200)]
        public string? WorkName { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "ESR must be a positive value.")]
        public decimal? Esr { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Rate must be a positive value.")]
        public decimal? Rate { get; set; }
    }

    public class SubContractorUpdateDto
    {
        [Required]
        [MaxLength(200)]
        public string SubContractorName { get; set; } = string.Empty;

        // Address
        [MaxLength(150)]
        public string? DoorNoAndStreetName { get; set; }

        [MaxLength(100)]
        public string? AreaName { get; set; }

        [MaxLength(100)]
        public string? Place { get; set; }

        [MaxLength(10)]
        public string? PinCode { get; set; }

        [MaxLength(100)]
        public string? CityOrTalukName { get; set; }

        [MaxLength(150)]
        public string? DistrictAndStateName { get; set; }

        // Contact
        [Phone]
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [Phone]
        [MaxLength(20)]
        public string? MobileNumber { get; set; }

        [MaxLength(20)]
        public string? FaxNumber { get; set; }

        [EmailAddress]
        [MaxLength(200)]
        public string? EmailId { get; set; }

        [Url]
        [MaxLength(200)]
        public string? WebsiteName { get; set; }

        // Bank
        [MaxLength(30)]
        public string? AccountNumber { get; set; }

        [MaxLength(100)]
        public string? AccountName { get; set; }

        [MaxLength(30)]
        public string? AccountType { get; set; }

        [MaxLength(100)]
        public string? BankName { get; set; }

        [MaxLength(100)]
        public string? BankBranch { get; set; }

        [MaxLength(20)]
        public string? BranchCode { get; set; }

        [MaxLength(11)]
        [RegularExpression(@"^[A-Z]{4}0[A-Z0-9]{6}$", ErrorMessage = "Invalid IFSC code format.")]
        public string? IfscCode { get; set; }

        // Tax
        [MaxLength(10)]
        [RegularExpression(@"^[A-Z]{5}[0-9]{4}[A-Z]{1}$", ErrorMessage = "Invalid PAN card format.")]
        public string? PanCardNumber { get; set; }

        [MaxLength(30)]
        public string? TinNumber { get; set; }

        [MaxLength(12)]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "Aadhaar number must be exactly 12 digits.")]
        public string? AadhaarNumber { get; set; }

        // Work
        [MaxLength(200)]
        public string? WorkName { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "ESR must be a positive value.")]
        public decimal? Esr { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Rate must be a positive value.")]
        public decimal? Rate { get; set; }

        public bool IsActive { get; set; }
    }

    public class SubContractorResponseDto
    {
        public int SubContractorId { get; set; }
        public string SubContractorName { get; set; } = string.Empty;

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
        public string? WebsiteName { get; set; }

        // Bank
        public string? BankName { get; set; }
        public string? BankBranch { get; set; }
        public string? AccountName { get; set; }
        public string? AccountType { get; set; }
        public string? IfscCode { get; set; }

        // Work
        public string? WorkName { get; set; }
        public decimal? Esr { get; set; }
        public decimal? Rate { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}