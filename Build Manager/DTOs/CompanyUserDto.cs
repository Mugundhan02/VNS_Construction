using System.ComponentModel.DataAnnotations;

namespace BuildManager.DTOs
{
    // ── Register DTOs ─────────────────────────────────────────────────────────

    public class RegisterRequestDto
    {
        [Required]
        [MaxLength(200)]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(250)]
        public string EmailId { get; set; } = string.Empty; // Added to register the email id

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    public class RegisterResponseDto
    {
        public int CompanyUserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
    }

    // ── Login DTOs ────────────────────────────────────────────────────────────

    public class LoginRequestDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }

    public class LoginResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
    }

    // ── Forgot Password DTOs ──────────────────────────────────────────────────

    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string EmailId { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string NewPassword { get; set; } = string.Empty;

        [Required]
        [Compare(nameof(NewPassword), ErrorMessage = "Passwords do not match.")]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }

    // ── Company User CRUD DTOs ────────────────────────────────────────────────

    public class CompanyUserRequestDto
    {
        [Required]
        public int CompanyId { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string UserType { get; set; } = "User";
    }

    public class CompanyUserUpdateDto
    {
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string UserType { get; set; } = "User";

        public bool IsActive { get; set; }
    }

    public class CompanyUserResponseDto
    {
        public int CompanyUserId { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string UserType { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}