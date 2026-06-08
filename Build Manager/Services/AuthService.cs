using System.Security.Cryptography;
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
        private readonly ITokenService         _tokenService;
        private readonly IPasswordService      _passwordService;

        private static readonly Dictionary<string, IEnumerable<string>> RolePermissions = new()
        {
            ["Owner"] = new[]
            {
                "masters.company-settings", "masters.company-user", "masters.company-bank",
                "masters.office-expense",   "masters.payment-types","masters.whom",
                "masters.installment-term", "masters.client",       "masters.supplier",
                "masters.subcontractor",    "masters.material",     "masters.jobwork",
                "transactions.new-transaction", "transactions.stock-transfer",
                "transactions.stock-analysis",  "transactions.estimated-qty",
                "transactions.installment-term",
                "reports.company", "reports.client", "reports.supplier",
                "reports.material","reports.labour-job"
            },
            ["Admin"] = new[]
            {
                "masters.company-settings", "masters.client",    "masters.supplier",
                "masters.subcontractor",    "masters.material",  "masters.jobwork",
                "masters.office-expense",   "masters.payment-types", "masters.whom",
                "masters.installment-term",
                "transactions.new-transaction", "transactions.stock-analysis",
                "transactions.installment-term",
                "reports.company", "reports.client", "reports.supplier",
                "reports.material","reports.labour-job"
            },
            ["User"] = new[]
            {
                "transactions.new-transaction", "transactions.stock-analysis",
                "transactions.installment-term",
                "reports.client", "reports.supplier"
            }
        };

        public AuthService(
            BuildManagerDbContext context,
            ITokenService         tokenService,
            IPasswordService      passwordService)
        {
            _context         = context;
            _tokenService    = tokenService;
            _passwordService = passwordService;
        }

        // ── Register ──────────────────────────────────────────────────────────

        public async Task<RegisterResponseDto> Register(RegisterRequestDto dto)
        {
            bool exists = await _context.CompanyUsers
                .AnyAsync(u => u.UserName == dto.UserName);

            if (exists)
                throw new DuplicateEntityException("User", "username", dto.UserName);

            // Create the company first
            var company = new Company { CompanyName = dto.CompanyName };
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            // Generate unique 512-bit salt and compute HMAC-SHA512 hash
            var salt = _passwordService.GenerateSalt();
            var hash = _passwordService.HashPassword(dto.Password, salt);

            var user = new CompanyUser
            {
                CompanyId    = company.CompanyId,
                UserName     = dto.UserName,
                PasswordHash = Convert.ToBase64String(hash),
                PasswordSalt = Convert.ToBase64String(salt),
                UserType     = "Owner",
                IsActive     = true
            };

            _context.CompanyUsers.Add(user);
            await _context.SaveChangesAsync();

            return new RegisterResponseDto
            {
                CompanyUserId = user.CompanyUserId,
                UserName      = user.UserName,
                UserType      = user.UserType,
                CompanyName   = company.CompanyName
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

            if (!_passwordService.VerifyPassword(dto.Password, storedHash, storedSalt))
                throw new UnAuthorizedException("Invalid username or password.");

            var jwt          = await _tokenService.GenerateToken(user);
            var refreshToken = await CreateRefreshToken(user.CompanyUserId);

            return BuildResponse(user, jwt, refreshToken.Token);
        }

        // ── Refresh Token ─────────────────────────────────────────────────────

        public async Task<LoginResponseDto?> RefreshToken(RefreshTokenRequestDto dto)
        {
            var stored = await _context.RefreshTokens
                .Include(r => r.CompanyUser)
                    .ThenInclude(u => u.Company)
                .FirstOrDefaultAsync(r => r.Token    == dto.RefreshToken
                                       && !r.IsRevoked
                                       && r.ExpiresAt > DateTime.UtcNow);

            if (stored is null) return null;

            // Rotate — revoke old, issue new
            stored.IsRevoked = true;
            var newRefresh   = await CreateRefreshToken(stored.CompanyUserId);
            var jwt          = await _tokenService.GenerateToken(stored.CompanyUser);
            await _context.SaveChangesAsync();

            return BuildResponse(stored.CompanyUser, jwt, newRefresh.Token);
        }

        // ── Revoke / Logout ───────────────────────────────────────────────────

        public async Task<bool> RevokeToken(string refreshToken)
        {
            var stored = await _context.RefreshTokens
                .FirstOrDefaultAsync(r => r.Token == refreshToken && !r.IsRevoked);

            if (stored is null) return false;

            stored.IsRevoked = true;
            await _context.SaveChangesAsync();
            return true;
        }

        // ── Change Password ───────────────────────────────────────────────────

        public async Task<bool> ChangePassword(int companyUserId, ChangePasswordRequestDto dto)
        {
            var user = await _context.CompanyUsers.FindAsync(companyUserId)
                ?? throw new EntityNotFoundException("User", companyUserId);

            var storedHash = Convert.FromBase64String(user.PasswordHash);
            var storedSalt = Convert.FromBase64String(user.PasswordSalt);

            if (!_passwordService.VerifyPassword(dto.CurrentPassword, storedHash, storedSalt))
                throw new UnAuthorizedException("Current password is incorrect.");

            // Generate a brand-new salt on every password change
            var newSalt = _passwordService.GenerateSalt();
            var newHash = _passwordService.HashPassword(dto.NewPassword, newSalt);

            user.PasswordHash = Convert.ToBase64String(newHash);
            user.PasswordSalt = Convert.ToBase64String(newSalt);

            await _context.SaveChangesAsync();
            return true;
        }

        // ── Private Helpers ───────────────────────────────────────────────────

        private async Task<RefreshToken> CreateRefreshToken(int companyUserId)
        {
            var token = new RefreshToken
            {
                CompanyUserId = companyUserId,
                Token         = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                ExpiresAt     = DateTime.UtcNow.AddDays(7),
                CreatedAt     = DateTime.UtcNow,
                IsRevoked     = false
            };
            _context.RefreshTokens.Add(token);
            await _context.SaveChangesAsync();
            return token;
        }

        private static LoginResponseDto BuildResponse(
            CompanyUser user, string jwt, string refreshToken)
        {
            RolePermissions.TryGetValue(user.UserType, out var permissions);
            return new LoginResponseDto
            {
                Token        = jwt,
                RefreshToken = refreshToken,
                UserName     = user.UserName,
                UserType     = user.UserType,
                CompanyName  = user.Company?.CompanyName ?? string.Empty,
                ExpiresAt    = DateTime.UtcNow.AddHours(8),
                Permissions  = permissions ?? Enumerable.Empty<string>()
            };
        }
    }
}
