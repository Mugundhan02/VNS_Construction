using BuildManager.Contexts;
using BuildManager.DTOs;
using BuildManager.Exceptions;
using BuildManager.Interfaces;
using BuildManager.Models;
using Microsoft.EntityFrameworkCore;

namespace BuildManager.Services
{
    public class AuthService : IAuthService
    {
        private readonly BuildManagerDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly IPasswordService _passwordService;

        public AuthService(
            BuildManagerDbContext context,
            ITokenService tokenService,
            IPasswordService passwordService)
        {
            _context = context;
            _tokenService = tokenService;
            _passwordService = passwordService;
        }

        // ── Register ──────────────────────────────────────────────────────────

        public async Task<RegisterResponseDto> Register(RegisterRequestDto dto)
        {
            bool exists = await _context.CompanyUsers
                .AnyAsync(u => u.UserName == dto.UserName);

            if (exists)
                throw new DuplicateEntityException("User", "username", dto.UserName);

            // Create the primary company tenant record first
            var company = new Company { CompanyName = dto.CompanyName };
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            // Generate cryptographically secure salt and calculate HMAC-SHA512 hash
            var salt = _passwordService.GenerateSalt();
            var hash = _passwordService.HashPassword(dto.Password, salt);

            var user = new CompanyUser
            {
                CompanyId = company.CompanyId,
                UserName = dto.UserName,
                EmailId = dto.EmailId,
                PasswordHash = Convert.ToBase64String(hash),
                PasswordSalt = Convert.ToBase64String(salt),
                UserType = "Owner", // First registered user is automatically designated as Owner
                IsActive = true
            };

            _context.CompanyUsers.Add(user);
            await _context.SaveChangesAsync();

            return new RegisterResponseDto
            {
                CompanyUserId = user.CompanyUserId,
                UserName = user.UserName,
                Password = dto.Password,
                CompanyName = company.CompanyName
            };
        }

        // ── Login ─────────────────────────────────────────────────────────────

        public async Task<LoginResponseDto> Login(LoginRequestDto dto)
        {
            var user = await _context.CompanyUsers
                .Include(u => u.Company)
                .FirstOrDefaultAsync(u => u.UserName == dto.UserName && u.IsActive);

            if (user is null)
                throw new UnAuthorizedException("Invalid username or password.");

            var storedHash = Convert.FromBase64String(user.PasswordHash);
            var storedSalt = Convert.FromBase64String(user.PasswordSalt);

            // Constant-time validation check against timing exploits
            if (!_passwordService.VerifyPassword(dto.Password, storedHash, storedSalt))
                throw new UnAuthorizedException("Invalid username or password.");

            // Mint a fresh secure JSON Web Token containing identity claims
            var jwt = await _tokenService.GenerateToken(user);

            return new LoginResponseDto
            {
                Token = jwt,
                UserName = user.UserName,
                CompanyName = user.Company?.CompanyName ?? string.Empty
            };
        }

        // ── Forgot Password ───────────────────────────────────────────────────

        public async Task<string> ForgotPassword(ForgotPasswordDto dto)
        {
            // Identity validation lookup matching the verified EmailId column properties
            var user = await _context.CompanyUsers
                .FirstOrDefaultAsync(u => u.EmailId == dto.EmailId)
                ?? throw new UnAuthorizedException("No system profile matches the given email identity.");

            // Generate a fresh salt and hash sequence to rewrite credential states safely
            var newSalt = _passwordService.GenerateSalt();
            var newHash = _passwordService.HashPassword(dto.NewPassword, newSalt);

            user.PasswordHash = Convert.ToBase64String(newHash);
            user.PasswordSalt = Convert.ToBase64String(newSalt);

            await _context.SaveChangesAsync();
            return "Password has been successfully modified.";
        }
    }
}