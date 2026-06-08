namespace BuildManager.Models
{
    public class AuditLog
    {
        public int      AuditLogId   { get; set; }
        public string   UserName     { get; set; } = string.Empty;
        public string   Action       { get; set; } = string.Empty;
        public string   EntityType   { get; set; } = string.Empty;
        public string?  EntityId     { get; set; }
        public string?  Description  { get; set; }
        public string?  IpAddress    { get; set; }
        public DateTime CreatedAt    { get; set; } = DateTime.UtcNow;
    }
}
