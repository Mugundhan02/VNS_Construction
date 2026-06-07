using BuildManager.DTOs;

namespace BuildManager.Services.Interfaces
{
    public interface IJobWorkService
    {
        Task<IEnumerable<JobWorkResponseDto>> GetAllAsync();
        Task<JobWorkResponseDto?> GetByIdAsync(int id);
        Task<JobWorkResponseDto> CreateAsync(JobWorkRequestDto dto);
        Task<JobWorkResponseDto?> UpdateAsync(int id, JobWorkRequestDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
