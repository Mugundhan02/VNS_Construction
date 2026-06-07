using AutoMapper;
using BuildManager.Data;
using BuildManager.DTOs;
using BuildManager.Models;
using BuildManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BuildManager.Services.Implementations
{
    public class JobWorkService : IJobWorkService
    {
        private readonly BuildManagerDbContext _context;
        private readonly IMapper _mapper;

        public JobWorkService(BuildManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper  = mapper;
        }

        public async Task<IEnumerable<JobWorkResponseDto>> GetAllAsync()
        {
            var jobWorks = await _context.JobWorks
                .AsNoTracking()
                .OrderBy(j => j.JobWorkName)
                .ToListAsync();

            return _mapper.Map<IEnumerable<JobWorkResponseDto>>(jobWorks);
        }

        public async Task<JobWorkResponseDto?> GetByIdAsync(int id)
        {
            var jobWork = await _context.JobWorks
                .AsNoTracking()
                .FirstOrDefaultAsync(j => j.JobWorkId == id);

            return jobWork is null ? null : _mapper.Map<JobWorkResponseDto>(jobWork);
        }

        public async Task<JobWorkResponseDto> CreateAsync(JobWorkRequestDto dto)
        {
            var jobWork = _mapper.Map<JobWork>(dto);
            _context.JobWorks.Add(jobWork);
            await _context.SaveChangesAsync();
            return _mapper.Map<JobWorkResponseDto>(jobWork);
        }

        public async Task<JobWorkResponseDto?> UpdateAsync(int id, JobWorkRequestDto dto)
        {
            var jobWork = await _context.JobWorks.FindAsync(id);
            if (jobWork is null) return null;

            _mapper.Map(dto, jobWork);
            await _context.SaveChangesAsync();
            return _mapper.Map<JobWorkResponseDto>(jobWork);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var jobWork = await _context.JobWorks.FindAsync(id);
            if (jobWork is null) return false;

            _context.JobWorks.Remove(jobWork);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
