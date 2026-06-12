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
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public CompanyUserService(
            BuildManagerDbContext context,
            IMapper mapper,
            IPasswordService passwordService)
        {
            _context = context;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        public async Task<IEnumerable<CompanyUserResponseDto>> GetAll()
        {
            var list = await _context.CompanyUsers
                .AsNoTracking()
                .Include(u => u.Company)
                .OrderBy(u => u.UserName)
                .ToListAsync();
            return _mapper.Map<IEnumerable<CompanyUserResponseDto>>(list);
        }

        public async Task<CompanyUserResponseDto> GetById(int id)
        {
            var entity = await _context.CompanyUsers
                .AsNoTracking()
                .Include(u => u.Company)
                .FirstOrDefaultAsync(u => u.CompanyUserId == id)
                ?? throw new EntityNotFoundException("CompanyUser", id);
            return _mapper.Map<CompanyUserResponseDto>(entity);
        }

        public async Task<CompanyUserResponseDto> Create(CompanyUserRequestDto dto)
        {
            bool exists = await _context.CompanyUsers.AnyAsync(u => u.UserName == dto.UserName);
            if (exists)
                throw new DuplicateEntityException("User", "username", dto.UserName);

            var salt = _passwordService.GenerateSalt();
            var hash = _passwordService.HashPassword(dto.Password, salt);

            var entity = _mapper.Map<CompanyUser>(dto);
            entity.PasswordHash = Convert.ToBase64String(hash);
            entity.PasswordSalt = Convert.ToBase64String(salt);
            entity.IsActive = true;

            _context.CompanyUsers.Add(entity);
            await _context.SaveChangesAsync();
            await _context.Entry(entity).Reference(u => u.Company).LoadAsync();
            return _mapper.Map<CompanyUserResponseDto>(entity);
        }

        public async Task<CompanyUserResponseDto> Update(int id, CompanyUserUpdateDto dto)
        {
            var entity = await _context.CompanyUsers
                .Include(u => u.Company)
                .FirstOrDefaultAsync(u => u.CompanyUserId == id)
                ?? throw new EntityNotFoundException("CompanyUser", id);

            _mapper.Map(dto, entity);
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