using BuildManager.DTOs;

namespace BuildManager.Interfaces
{
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierResponseDto>> GetAll();
        Task<SupplierResponseDto> GetById(int id);
        Task<SupplierResponseDto> Create(SupplierRequestDto dto);
        Task<SupplierResponseDto> Update(int id, SupplierRequestDto dto);
        Task<bool> Delete(int id);
    }
}