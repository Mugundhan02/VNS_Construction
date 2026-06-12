using AutoMapper;
using BuildManager.Contexts;
using BuildManager.DTOs;
using BuildManager.Exceptions;
using BuildManager.Interfaces;
using BuildManager.Models;
using Microsoft.EntityFrameworkCore;

namespace BuildManager.Services
{
    public class JobWorkService : IJobWorkService
    {
        private readonly BuildManagerDbContext _context;
        private readonly IMapper _mapper;

        public JobWorkService(BuildManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<JobWorkResponseDto>> GetAll()
        {
            var list = await _context.JobWorks.AsNoTracking().OrderBy(j => j.JobWorkName).ToListAsync();
            return _mapper.Map<IEnumerable<JobWorkResponseDto>>(list);
        }

        public async Task<JobWorkResponseDto> GetById(int id)
        {
            var entity = await _context.JobWorks.AsNoTracking().FirstOrDefaultAsync(j => j.JobWorkId == id)
                         ?? throw new EntityNotFoundException("JobWork", id);
            return _mapper.Map<JobWorkResponseDto>(entity);
        }

        public async Task<JobWorkResponseDto> Create(JobWorkRequestDto dto)
        {
            var entity = _mapper.Map<JobWork>(dto);
            _context.JobWorks.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<JobWorkResponseDto>(entity);
        }

        public async Task<JobWorkResponseDto> Update(int id, JobWorkRequestDto dto)
        {
            var entity = await _context.JobWorks.FindAsync(id)
                         ?? throw new EntityNotFoundException("JobWork", id);
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<JobWorkResponseDto>(entity);
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _context.JobWorks.FindAsync(id)
                         ?? throw new EntityNotFoundException("JobWork", id);
            _context.JobWorks.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}