using AutoMapper;
using BuildManager.Contexts;
using BuildManager.DTOs;
using BuildManager.Exceptions;
using BuildManager.Interfaces;
using BuildManager.Models;
using Microsoft.EntityFrameworkCore;

namespace BuildManager.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly BuildManagerDbContext _context;
        private readonly IMapper               _mapper;

        public CompanyService(BuildManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper  = mapper;
        }

        public async Task<IEnumerable<CompanyResponseDto>> GetAll()
        {
            var list = await _context.Companies.AsNoTracking().OrderBy(c => c.CompanyName).ToListAsync();
            return _mapper.Map<IEnumerable<CompanyResponseDto>>(list);
        }

        public async Task<CompanyResponseDto> GetById(int id)
        {
            var entity = await _context.Companies.AsNoTracking().FirstOrDefaultAsync(c => c.CompanyId == id)
                         ?? throw new EntityNotFoundException("Company", id);
            return _mapper.Map<CompanyResponseDto>(entity);
        }

        public async Task<CompanyResponseDto> Create(CompanyRequestDto dto)
        {
            var entity = _mapper.Map<Company>(dto);
            _context.Companies.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CompanyResponseDto>(entity);
        }

        public async Task<CompanyResponseDto> Update(int id, CompanyRequestDto dto)
        {
            var entity = await _context.Companies.FindAsync(id)
                         ?? throw new EntityNotFoundException("Company", id);
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CompanyResponseDto>(entity);
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _context.Companies.FindAsync(id)
                         ?? throw new EntityNotFoundException("Company", id);
            _context.Companies.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
