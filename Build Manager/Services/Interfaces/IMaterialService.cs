using BuildManager.DTOs;

namespace BuildManager.Services.Interfaces
{
    public interface IMaterialService
    {
        Task<IEnumerable<MaterialResponseDto>> GetAllAsync();
        Task<MaterialResponseDto?> GetByIdAsync(int id);
        Task<MaterialResponseDto> CreateAsync(MaterialRequestDto dto);
        Task<MaterialResponseDto?> UpdateAsync(int id, MaterialRequestDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
