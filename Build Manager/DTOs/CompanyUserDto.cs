using System.ComponentModel.DataAnnotations;

namespace BuildManager.DTOs
{
    public class CompanyUserRequestDto
    {
        [Required]
        public int CompanyId { get; set; }

        [Required, MaxLength(100)]
        public string UserName { get; set; } = string.Empty;

        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;

        /// <summary>Owner | Admin | User</summary>
        [Required]
        public string UserType { get; set; } = "User";
    }

    public class CompanyUserResponseDto
    {
        public int    CompanyUserId { get; set; }
        public int    CompanyId    { get; set; }
        public string CompanyName  { get; set; } = string.Empty;
        public string UserName     { get; set; } = string.Empty;
        public string UserType     { get; set; } = string.Empty;
        public bool   IsActive     { get; set; }
    }

    public class LoginRequestDto
    {
        [Required] public string UserName { get; set; } = string.Empty;
        [Required] public string Password { get; set; } = string.Empty;
    }

    public class LoginResponseDto
    {
        public string Token       { get; set; } = string.Empty;
        public string UserName    { get; set; } = string.Empty;
        public string UserType    { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
    }
}
