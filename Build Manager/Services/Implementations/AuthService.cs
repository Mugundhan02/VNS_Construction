using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BuildManager.Data;
using BuildManager.DTOs;
using BuildManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BuildManager.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly BuildManagerDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(BuildManagerDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
        {
            var user = await _context.CompanyUsers
                .Include(u => u.Company)
                .FirstOrDefaultAsync(u => u.UserName == request.UserName && u.IsActive);

            if (user == null)
                return null;

            // Verify hashed password
            bool isValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            if (!isValid)
                return null;

            var token = GenerateJwtToken(user.UserName, user.UserType, user.CompanyUserId);

            return new LoginResponseDto
            {
                Token       = token,
                UserName    = user.UserName,
                UserType    = user.UserType,
                CompanyName = user.Company?.CompanyName ?? string.Empty
            };
        }

        private string GenerateJwtToken(string userName, string userType, int userId)
        {
            var jwtKey    = _configuration["Jwt:Key"]    ?? "BuildManagerDefaultSecretKey2024!";
            var jwtIssuer = _configuration["Jwt:Issuer"] ?? "BuildManager";

            var claims = new[]
            {
                new Claim(ClaimTypes.Name,             userName),
                new Claim(ClaimTypes.Role,             userType),
                new Claim(ClaimTypes.NameIdentifier,   userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key   = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer:             jwtIssuer,
                audience:           jwtIssuer,
                claims:             claims,
                expires:            DateTime.UtcNow.AddHours(8),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
