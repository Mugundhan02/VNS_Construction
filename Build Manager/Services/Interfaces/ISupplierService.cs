using BuildManager.DTOs;

namespace BuildManager.Services.Interfaces
{
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierResponseDto>> GetAllAsync();
        Task<SupplierResponseDto?> GetByIdAsync(int id);
        Task<SupplierResponseDto> CreateAsync(SupplierRequestDto dto);
        Task<SupplierResponseDto?> UpdateAsync(int id, SupplierRequestDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
