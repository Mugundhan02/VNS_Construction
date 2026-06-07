using System.ComponentModel.DataAnnotations;

namespace BuildManager.DTOs
{
    public class CompanyRequestDto
    {
        [Required, MaxLength(200)]
        public string CompanyName { get; set; } = string.Empty;

        public string? DoorNoAndStreetName  { get; set; }
        public string? AreaName             { get; set; }
        public string? Place                { get; set; }

        [MaxLength(10)]
        public string? PinCode              { get; set; }
        public string? CityOrTalukName      { get; set; }
        public string? DistrictAndStateName { get; set; }

        [MaxLength(20)] public string? PhoneNumber  { get; set; }
        [MaxLength(20)] public string? MobileNumber { get; set; }
        [MaxLength(20)] public string? FaxNumber    { get; set; }

        [EmailAddress, MaxLength(200)]
        public string? EmailId     { get; set; }
        [MaxLength(200)]
        public string? WebsiteName { get; set; }

        [MaxLength(20)] public string? PanCardNumber { get; set; }
        [MaxLength(30)] public string? TinNumber     { get; set; }
        [MaxLength(30)] public string? CstNumber     { get; set; }
    }

    public class CompanyResponseDto
    {
        public int    CompanyId             { get; set; }
        public string CompanyName           { get; set; } = string.Empty;
        public string? DoorNoAndStreetName  { get; set; }
        public string? AreaName             { get; set; }
        public string? Place                { get; set; }
        public string? PinCode              { get; set; }
        public string? CityOrTalukName      { get; set; }
        public string? DistrictAndStateName { get; set; }
        public string? PhoneNumber          { get; set; }
        public string? MobileNumber         { get; set; }
        public string? FaxNumber            { get; set; }
        public string? EmailId              { get; set; }
        public string? WebsiteName          { get; set; }
        public string? PanCardNumber        { get; set; }
        public string? TinNumber            { get; set; }
        public string? CstNumber            { get; set; }
    }
}
