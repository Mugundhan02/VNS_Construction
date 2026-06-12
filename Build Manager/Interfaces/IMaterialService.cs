using BuildManager.DTOs;

namespace BuildManager.Interfaces
{
    public interface IMaterialService
    {
        Task<IEnumerable<MaterialResponseDto>> GetAll();
        Task<MaterialResponseDto> GetById(int id);
        Task<MaterialResponseDto> Create(MaterialRequestDto dto);
        Task<MaterialResponseDto> Update(int id, MaterialRequestDto dto);
        Task<bool> Delete(int id);
    }
}