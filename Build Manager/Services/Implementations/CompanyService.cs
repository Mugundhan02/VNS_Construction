using AutoMapper;
using BuildManager.Data;
using BuildManager.DTOs;
using BuildManager.Models;
using BuildManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BuildManager.Services.Implementations
{
    public class CompanyService : ICompanyService
    {
        private readonly BuildManagerDbContext _context;
        private readonly IMapper _mapper;

        public CompanyService(BuildManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper  = mapper;
        }

        public async Task<IEnumerable<CompanyResponseDto>> GetAllAsync()
        {
            var companies = await _context.Companies
                .AsNoTracking()
                .OrderBy(c => c.CompanyName)
                .ToListAsync();

            return _mapper.Map<IEnumerable<CompanyResponseDto>>(companies);
        }

        public async Task<CompanyResponseDto?> GetByIdAsync(int id)
        {
            var company = await _context.Companies
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CompanyId == id);

            return company is null ? null : _mapper.Map<CompanyResponseDto>(company);
        }

        public async Task<CompanyResponseDto> CreateAsync(CompanyRequestDto dto)
        {
            var company = _mapper.Map<Company>(dto);
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return _mapper.Map<CompanyResponseDto>(company);
        }

        public async Task<CompanyResponseDto?> UpdateAsync(int id, CompanyRequestDto dto)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company is null) return null;

            _mapper.Map(dto, company);
            await _context.SaveChangesAsync();
            return _mapper.Map<CompanyResponseDto>(company);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company is null) return false;

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
