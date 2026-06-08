namespace BuildManager.Interfaces
{
    public interface IAuditLogService
    {
        Task LogAsync(
            string  userName,
            string  action,
            string  entityType,
            string? entityId,
            string? description,
            string? ipAddress);
    }
}
