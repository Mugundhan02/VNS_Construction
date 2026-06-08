using BuildManager.Models;

namespace BuildManager.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken(CompanyUser user);
    }
}
