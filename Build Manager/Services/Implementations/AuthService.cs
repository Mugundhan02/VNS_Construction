using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BuildManager.Data;
using BuildManager.DTOs;
using BuildManager.Models;
using BuildManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BuildManager.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly BuildManagerDbContext _context;
        private readonly IConfiguration        _configuration;

        // ── Role → permitted menu sections (mirrors the screen images) ─────────
        // Owner  : everything
        // Admin  : masters, transactions (no stock-transfer), reports
        // User   : transactions (new-transaction, stock-analysis, installment-term), reports (read-only)
        private static readonly Dictionary<string, IEnumerable<string>> RolePermissions = new()
        {
            ["Owner"] = new[]
            {
                "masters.company-settings",
                "masters.company-user",
                "masters.company-bank",
                "masters.office-expense",
                "masters.payment-types",
                "masters.whom",
                "masters.installment-term",
                "masters.client",
                "masters.supplier",
                "masters.subcontractor",
                "masters.material",
                "masters.jobwork",
                "transactions.new-transaction",
                "transactions.stock-transfer",
                "transactions.stock-analysis",
                "transactions.estimated-qty",
                "transactions.installment-term",
                "reports.company",
                "reports.client",
                "reports.supplier",
                "reports.material",
                "reports.labour-job"
            },
            ["Admin"] = new[]
            {
                "masters.company-settings",
                "masters.client",
                "masters.supplier",
                "masters.subcontractor",
                "masters.material",
                "masters.jobwork",
                "masters.office-expense",
                "masters.payment-types",
                "masters.whom",
                "masters.installment-term",
                "transactions.new-transaction",
                "transactions.stock-analysis",
                "transactions.installment-term",
                "reports.company",
                "reports.client",
                "reports.supplier",
                "reports.material",
                "reports.labour-job"
            },
            ["User"] = new[]
            {
                "transactions.new-transaction",
                "transactions.stock-analysis",
                "transactions.installment-term",
                "reports.client",
                "reports.supplier"
            }
        };

        public AuthService(BuildManagerDbContext context, IConfiguration configuration)
        {
            _context       = context;
            _configuration = configuration;
        }

        // ── Register ─────────────────────────────────────────────────────────

        public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto dto)
        {
            // Check duplicate username
            bool userExists = await _context.CompanyUsers
                .AnyAsync(u => u.UserName == dto.UserName);

            if (userExists)
                throw new InvalidOperationException($"Username '{dto.UserName}' is already taken.");

            // Create company first
            var company = new Company { CompanyName = dto.CompanyName };
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            // Create the Owner user for that company
            var user = new CompanyUser
            {
                CompanyId    = company.CompanyId,
                UserName     = dto.UserName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
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

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto dto)
        {
            var user = await _context.CompanyUsers
                .Include(u => u.Company)
                .FirstOrDefaultAsync(u => u.UserName == dto.UserName && u.IsActive);

            if (user is null)
                return null;

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return null;

            var (jwt, jwtExpiry) = GenerateJwtToken(user);
            var refreshToken     = await CreateRefreshTokenAsync(user.CompanyUserId);

            return BuildLoginResponse(user, jwt, jwtExpiry, refreshToken.Token);
        }

        // ── Refresh Token ─────────────────────────────────────────────────────

        public async Task<LoginResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto dto)
        {
            var stored = await _context.RefreshTokens
                .Include(r => r.CompanyUser)
                    .ThenInclude(u => u.Company)
                .FirstOrDefaultAsync(r => r.Token == dto.RefreshToken
                                       && !r.IsRevoked
                                       && r.ExpiresAt > DateTime.UtcNow);

            if (stored is null)
                return null;

            // Rotate: revoke old, issue new
            stored.IsRevoked = true;
            var newRefresh         = await CreateRefreshTokenAsync(stored.CompanyUserId);
            var (jwt, jwtExpiry)   = GenerateJwtToken(stored.CompanyUser);
            await _context.SaveChangesAsync();

            return BuildLoginResponse(stored.CompanyUser, jwt, jwtExpiry, newRefresh.Token);
        }

        // ── Revoke (Logout) ───────────────────────────────────────────────────

        public async Task<bool> RevokeTokenAsync(string refreshToken)
        {
            var stored = await _context.RefreshTokens
                .FirstOrDefaultAsync(r => r.Token == refreshToken && !r.IsRevoked);

            if (stored is null)
                return false;

            stored.IsRevoked = true;
            await _context.SaveChangesAsync();
            return true;
        }

        // ── Change Password ───────────────────────────────────────────────────

        public async Task<bool> ChangePasswordAsync(int companyUserId, ChangePasswordRequestDto dto)
        {
            var user = await _context.CompanyUsers.FindAsync(companyUserId);
            if (user is null) return false;

            if (!BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, user.PasswordHash))
                throw new UnauthorizedAccessException("Current password is incorrect.");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            await _context.SaveChangesAsync();
            return true;
        }

        // ── Private Helpers ───────────────────────────────────────────────────

        private (string token, DateTime expiry) GenerateJwtToken(CompanyUser user)
        {
            var jwtKey    = _configuration["Jwt:Key"]    ?? "BuildManager@VNSConstruction#SecretKey2024!";
            var jwtIssuer = _configuration["Jwt:Issuer"] ?? "BuildManager";
            var expiry    = DateTime.UtcNow.AddHours(8);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.CompanyUserId.ToString()),
                new Claim(ClaimTypes.Name,           user.UserName),
                new Claim(ClaimTypes.Role,           user.UserType),
                new Claim("companyId",               user.CompanyId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key   = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer:             jwtIssuer,
                audience:           jwtIssuer,
                claims:             claims,
                expires:            expiry,
                signingCredentials: creds);

            return (new JwtSecurityTokenHandler().WriteToken(token), expiry);
        }

        private async Task<RefreshToken> CreateRefreshTokenAsync(int companyUserId)
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

        private static LoginResponseDto BuildLoginResponse(
            CompanyUser user, string jwt, DateTime expiry, string refreshToken)
        {
            RolePermissions.TryGetValue(user.UserType, out var permissions);

            return new LoginResponseDto
            {
                Token        = jwt,
                RefreshToken = refreshToken,
                UserName     = user.UserName,
                UserType     = user.UserType,
                CompanyName  = user.Company?.CompanyName ?? string.Empty,
                ExpiresAt    = expiry,
                Permissions  = permissions ?? Enumerable.Empty<string>()
            };
        }
    }
}
