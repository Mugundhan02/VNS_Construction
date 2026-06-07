using BuildManager.DTOs;

namespace BuildManager.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyResponseDto>> GetAllAsync();
        Task<CompanyResponseDto?> GetByIdAsync(int id);
        Task<CompanyResponseDto> CreateAsync(CompanyRequestDto dto);
        Task<CompanyResponseDto?> UpdateAsync(int id, CompanyRequestDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
