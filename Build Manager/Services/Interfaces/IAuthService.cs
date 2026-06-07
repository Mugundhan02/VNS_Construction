using BuildManager.DTOs;

namespace BuildManager.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto request);
    }
}
