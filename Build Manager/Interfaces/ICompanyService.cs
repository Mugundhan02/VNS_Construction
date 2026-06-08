using BuildManager.DTOs;

namespace BuildManager.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyResponseDto>> GetAll();
        Task<CompanyResponseDto> GetById(int id);
        Task<CompanyResponseDto> Create(CompanyRequestDto dto);
        Task<CompanyResponseDto> Update(int id, CompanyRequestDto dto);
        Task<bool> Delete(int id);
    }
}
