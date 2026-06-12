using BuildManager.DTOs;

namespace BuildManager.Interfaces
{
    public interface ISubContractorService
    {
        Task<IEnumerable<SubContractorResponseDto>> GetAll();
        Task<SubContractorResponseDto> GetById(int id);
        Task<SubContractorResponseDto> Create(SubContractorRequestDto dto);
        Task<SubContractorResponseDto> Update(int id, SubContractorRequestDto dto);
        Task<bool> Delete(int id);
    }
}