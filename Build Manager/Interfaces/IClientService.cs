using BuildManager.DTOs;

namespace BuildManager.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<ClientResponseDto>> GetAll();
        Task<ClientResponseDto> GetById(int id);
        Task<ClientResponseDto> Create(ClientRequestDto dto);
        Task<ClientResponseDto> Update(int id, ClientRequestDto dto);
        Task<bool> Delete(int id);
    }
}