using BuildManager.DTOs;

namespace BuildManager.Services.Interfaces
{
    public interface ICompanyUserService
    {
        Task<IEnumerable<CompanyUserResponseDto>> GetAllAsync();
        Task<CompanyUserResponseDto?> GetByIdAsync(int id);
        Task<CompanyUserResponseDto> CreateAsync(CompanyUserRequestDto dto);
        Task<CompanyUserResponseDto?> UpdateAsync(int id, CompanyUserRequestDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
