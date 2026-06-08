using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BuildManager.Interfaces;
using BuildManager.Models;
using Microsoft.IdentityModel.Tokens;

namespace BuildManager.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<string> GenerateToken(CompanyUser user)
        {
            var jwtKey    = _configuration["Jwt:Key"]    ?? "BuildManager@VNSConstruction#SecretKey2024!";
            var jwtIssuer = _configuration["Jwt:Issuer"] ?? "BuildManager";

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
                expires:            DateTime.UtcNow.AddHours(8),
                signingCredentials: creds);

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
