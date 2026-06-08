using BuildManager.DTOs;

namespace BuildManager.Interfaces
{
    public interface ICompanyUserService
    {
        Task<IEnumerable<CompanyUserResponseDto>> GetAll();
        Task<CompanyUserResponseDto> GetById(int id);
        Task<CompanyUserResponseDto> Create(CompanyUserRequestDto dto);
        Task<CompanyUserResponseDto> Update(int id, CompanyUserRequestDto dto);
        Task<bool> Delete(int id);
    }
}
