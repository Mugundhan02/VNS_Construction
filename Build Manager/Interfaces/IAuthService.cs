using BuildManager.DTOs;

namespace BuildManager.Interfaces
{
    public interface IAuthService
    {
        Task<RegisterResponseDto> Register(RegisterRequestDto request);
        Task<LoginResponseDto> Login(LoginRequestDto request);
        Task<string> ForgotPassword(ForgotPasswordDto request);
    }
}