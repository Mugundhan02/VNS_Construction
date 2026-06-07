using BuildManager.DTOs;

namespace BuildManager.Services.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<ClientResponseDto>> GetAllAsync();
        Task<ClientResponseDto?> GetByIdAsync(int id);
        Task<ClientResponseDto> CreateAsync(ClientRequestDto dto);
        Task<ClientResponseDto?> UpdateAsync(int id, ClientRequestDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
