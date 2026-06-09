using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BuildManager.Models
{
    [Owned]
    public class Address
    {
        [MaxLength(150)] public string? DoorNoAndStreetName  { get; set; }
        [MaxLength(100)] public string? AreaName             { get; set; }
        [MaxLength(100)] public string? Place                { get; set; }
        [MaxLength(10)]  public string? PinCode              { get; set; }
        [MaxLength(100)] public string? CityOrTalukName      { get; set; }
        [MaxLength(150)] public string? DistrictAndStateName { get; set; }
    }

    [Owned]
    public class ContactInfo
    {
        [MaxLength(20), Phone]           public string? PhoneNumber  { get; set; }
        [MaxLength(20), Phone]           public string? MobileNumber { get; set; }
        [MaxLength(20)]                  public string? FaxNumber    { get; set; }
        [MaxLength(150), EmailAddress]   public string? EmailId      { get; set; }
        [MaxLength(200), Url]            public string? WebsiteName  { get; set; }
    }

    [Owned]
    public class BankDetails
    {
        [MaxLength(30)]  public string? AccountNumber { get; set; }
        [MaxLength(100)] public string? AccountName   { get; set; }
        [MaxLength(30)]  public string? AccountType   { get; set; }
        [MaxLength(100)] public string? BankName      { get; set; }
        [MaxLength(100)] public string? BankBranch    { get; set; }
        [MaxLength(20)]  public string? BranchCode    { get; set; }
        [MaxLength(11)]  public string? IfscCode      { get; set; }
    }

    [Owned]
    public class IdentityDetails
    {
        [MaxLength(10)]  public string? PanCardNumber { get; set; }
        [MaxLength(20)]  public string? TinNumber     { get; set; }
        [MaxLength(20)]  public string? CstNumber     { get; set; }
        [MaxLength(12)]  public string? AadhaarNumber { get; set; }
    }

    [Owned]
    public class EstimateDetails
    {
        [Range(0, double.MaxValue)] public decimal? Unit   { get; set; }
        [Range(0, double.MaxValue)] public decimal? Rate   { get; set; }
        [Range(0, double.MaxValue)] public decimal? Amount { get; set; }
    }

    [Owned]
    public class WorkDetails
    {
        [MaxLength(200)]            public string?  WorkName { get; set; }
        [Range(0, double.MaxValue)] public decimal? Esr      { get; set; }
        [Range(0, double.MaxValue)] public decimal? Rate     { get; set; }
    }
}
