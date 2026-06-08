using BuildManager.DTOs;

namespace BuildManager.Interfaces
{
    public interface IJobWorkService
    {
        Task<IEnumerable<JobWorkResponseDto>> GetAll();
        Task<JobWorkResponseDto> GetById(int id);
        Task<JobWorkResponseDto> Create(JobWorkRequestDto dto);
        Task<JobWorkResponseDto> Update(int id, JobWorkRequestDto dto);
        Task<bool> Delete(int id);
    }
}
