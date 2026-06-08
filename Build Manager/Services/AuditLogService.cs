using BuildManager.Contexts;
using BuildManager.Interfaces;
using BuildManager.Models;

namespace BuildManager.Services
{
    public class AuditLogService : IAuditLogService
    {
        private readonly BuildManagerDbContext _context;

        public AuditLogService(BuildManagerDbContext context)
        {
            _context = context;
        }

        public async Task LogAsync(
            string  userName,
            string  action,
            string  entityType,
            string? entityId,
            string? description,
            string? ipAddress)
        {
            _context.AuditLogs.Add(new AuditLog
            {
                UserName    = userName,
                Action      = action,
                EntityType  = entityType,
                EntityId    = entityId,
                Description = description,
                IpAddress   = ipAddress,
                CreatedAt   = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();
        }
    }
}
