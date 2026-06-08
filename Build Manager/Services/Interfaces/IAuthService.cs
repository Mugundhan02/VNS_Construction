using BuildManager.DTOs;

namespace BuildManager.Services.Interfaces
{
    public interface IAuthService
    {
        /// <summary>Register the first Owner for a new company.</summary>
        Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto dto);

        /// <summary>Login and return JWT + refresh token.</summary>
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto dto);

        /// <summary>Issue a new JWT using a valid refresh token.</summary>
        Task<LoginResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto dto);

        /// <summary>Revoke a refresh token on logout.</summary>
        Task<bool> RevokeTokenAsync(string refreshToken);

        /// <summary>Change password for the currently authenticated user.</summary>
        Task<bool> ChangePasswordAsync(int companyUserId, ChangePasswordRequestDto dto);
    }
}
