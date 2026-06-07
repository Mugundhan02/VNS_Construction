using BuildManager.DTOs;

namespace BuildManager.Services.Interfaces
{
    public interface ISubContractorService
    {
        Task<IEnumerable<SubContractorResponseDto>> GetAllAsync();
        Task<SubContractorResponseDto?> GetByIdAsync(int id);
        Task<SubContractorResponseDto> CreateAsync(SubContractorRequestDto dto);
        Task<SubContractorResponseDto?> UpdateAsync(int id, SubContractorRequestDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
