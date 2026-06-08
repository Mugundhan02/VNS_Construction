using AutoMapper;
using BuildManager.Contexts;
using BuildManager.DTOs;
using BuildManager.Exceptions;
using BuildManager.Interfaces;
using BuildManager.Models;
using Microsoft.EntityFrameworkCore;

namespace BuildManager.Services
{
    public class CompanyUserService : ICompanyUserService
    {
        private readonly BuildManagerDbContext _context;
        private readonly IMapper               _mapper;

        public CompanyUserService(BuildManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper  = mapper;
        }

        public async Task<IEnumerable<CompanyUserResponseDto>> GetAll()
        {
            var list = await _context.CompanyUsers.AsNoTracking()
                .Include(u => u.Company).OrderBy(u => u.UserName).ToListAsync();
            return _mapper.Map<IEnumerable<CompanyUserResponseDto>>(list);
        }

        public async Task<CompanyUserResponseDto> GetById(int id)
        {
            var entity = await _context.CompanyUsers.AsNoTracking()
                .Include(u => u.Company).FirstOrDefaultAsync(u => u.CompanyUserId == id)
                ?? throw new EntityNotFoundException("CompanyUser", id);
            return _mapper.Map<CompanyUserResponseDto>(entity);
        }

        public async Task<CompanyUserResponseDto> Create(CompanyUserRequestDto dto)
        {
            var entity = _mapper.Map<CompanyUser>(dto);
            entity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            _context.CompanyUsers.Add(entity);
            await _context.SaveChangesAsync();
            await _context.Entry(entity).Reference(u => u.Company).LoadAsync();
            return _mapper.Map<CompanyUserResponseDto>(entity);
        }

        public async Task<CompanyUserResponseDto> Update(int id, CompanyUserRequestDto dto)
        {
            var entity = await _context.CompanyUsers.Include(u => u.Company)
                .FirstOrDefaultAsync(u => u.CompanyUserId == id)
                ?? throw new EntityNotFoundException("CompanyUser", id);
            _mapper.Map(dto, entity);
            entity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            await _context.SaveChangesAsync();
            return _mapper.Map<CompanyUserResponseDto>(entity);
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _context.CompanyUsers.FindAsync(id)
                         ?? throw new EntityNotFoundException("CompanyUser", id);
            _context.CompanyUsers.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
