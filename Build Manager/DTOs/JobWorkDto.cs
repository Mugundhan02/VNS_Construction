using System.ComponentModel.DataAnnotations;

namespace BuildManager.DTOs
{
    public class JobWorkRequestDto
    {
        [Required, MaxLength(200)]
        public string JobWorkName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? Unit { get; set; }

        public decimal Rate { get; set; } = 0;
    }

    public class JobWorkResponseDto
    {
        public int    JobWorkId   { get; set; }
        public string JobWorkName { get; set; } = string.Empty;
        public string? Unit       { get; set; }
        public decimal Rate       { get; set; }
    }
}
