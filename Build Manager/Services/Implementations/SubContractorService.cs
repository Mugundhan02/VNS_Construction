using AutoMapper;
using BuildManager.Data;
using BuildManager.DTOs;
using BuildManager.Models;
using BuildManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BuildManager.Services.Implementations
{
    public class SubContractorService : ISubContractorService
    {
        private readonly BuildManagerDbContext _context;
        private readonly IMapper _mapper;

        public SubContractorService(BuildManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper  = mapper;
        }

        public async Task<IEnumerable<SubContractorResponseDto>> GetAllAsync()
        {
            var subContractors = await _context.SubContractors
                .AsNoTracking()
                .OrderBy(sc => sc.SubContractorName)
                .ToListAsync();

            return _mapper.Map<IEnumerable<SubContractorResponseDto>>(subContractors);
        }

        public async Task<SubContractorResponseDto?> GetByIdAsync(int id)
        {
            var subContractor = await _context.SubContractors
                .AsNoTracking()
                .FirstOrDefaultAsync(sc => sc.SubContractorId == id);

            return subContractor is null ? null : _mapper.Map<SubContractorResponseDto>(subContractor);
        }

        public async Task<SubContractorResponseDto> CreateAsync(SubContractorRequestDto dto)
        {
            var subContractor = _mapper.Map<SubContractor>(dto);
            _context.SubContractors.Add(subContractor);
            await _context.SaveChangesAsync();
            return _mapper.Map<SubContractorResponseDto>(subContractor);
        }

        public async Task<SubContractorResponseDto?> UpdateAsync(int id, SubContractorRequestDto dto)
        {
            var subContractor = await _context.SubContractors.FindAsync(id);
            if (subContractor is null) return null;

            _mapper.Map(dto, subContractor);
            await _context.SaveChangesAsync();
            return _mapper.Map<SubContractorResponseDto>(subContractor);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var subContractor = await _context.SubContractors.FindAsync(id);
            if (subContractor is null) return false;

            _context.SubContractors.Remove(subContractor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
