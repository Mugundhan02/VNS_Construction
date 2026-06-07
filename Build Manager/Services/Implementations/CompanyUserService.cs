using AutoMapper;
using BuildManager.Data;
using BuildManager.DTOs;
using BuildManager.Models;
using BuildManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BuildManager.Services.Implementations
{
    public class CompanyUserService : ICompanyUserService
    {
        private readonly BuildManagerDbContext _context;
        private readonly IMapper _mapper;

        public CompanyUserService(BuildManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper  = mapper;
        }

        public async Task<IEnumerable<CompanyUserResponseDto>> GetAllAsync()
        {
            var users = await _context.CompanyUsers
                .AsNoTracking()
                .Include(u => u.Company)
                .OrderBy(u => u.UserName)
                .ToListAsync();

            return _mapper.Map<IEnumerable<CompanyUserResponseDto>>(users);
        }

        public async Task<CompanyUserResponseDto?> GetByIdAsync(int id)
        {
            var user = await _context.CompanyUsers
                .AsNoTracking()
                .Include(u => u.Company)
                .FirstOrDefaultAsync(u => u.CompanyUserId == id);

            return user is null ? null : _mapper.Map<CompanyUserResponseDto>(user);
        }

        public async Task<CompanyUserResponseDto> CreateAsync(CompanyUserRequestDto dto)
        {
            var user = _mapper.Map<CompanyUser>(dto);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            _context.CompanyUsers.Add(user);
            await _context.SaveChangesAsync();

            // Reload with navigation for response
            await _context.Entry(user).Reference(u => u.Company).LoadAsync();
            return _mapper.Map<CompanyUserResponseDto>(user);
        }

        public async Task<CompanyUserResponseDto?> UpdateAsync(int id, CompanyUserRequestDto dto)
        {
            var user = await _context.CompanyUsers
                .Include(u => u.Company)
                .FirstOrDefaultAsync(u => u.CompanyUserId == id);

            if (user is null) return null;

            _mapper.Map(dto, user);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            await _context.SaveChangesAsync();
            return _mapper.Map<CompanyUserResponseDto>(user);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.CompanyUsers.FindAsync(id);
            if (user is null) return false;

            _context.CompanyUsers.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
