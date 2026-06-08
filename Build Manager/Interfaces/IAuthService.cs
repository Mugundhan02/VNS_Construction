using BuildManager.DTOs;

namespace BuildManager.Interfaces
{
    public interface IAuthService
    {
        Task<RegisterResponseDto> Register(RegisterRequestDto dto);
        Task<LoginResponseDto> Login(LoginRequestDto dto);
        Task<LoginResponseDto?> RefreshToken(RefreshTokenRequestDto dto);
        Task<bool> RevokeToken(string refreshToken);
        Task<bool> ChangePassword(int companyUserId, ChangePasswordRequestDto dto);
    }
}
