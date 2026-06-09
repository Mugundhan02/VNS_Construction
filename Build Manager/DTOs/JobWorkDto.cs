using System.ComponentModel.DataAnnotations;

namespace BuildManager.DTOs
{
    public class JobWorkRequestDto
    {
        [Required]
        [MaxLength(200)]
        public string JobWorkName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? Unit { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Rate must be a positive value.")]
        public decimal Rate { get; set; } = 0;
    }

    public class JobWorkUpdateDto
    {
        [Required]
        [MaxLength(200)]
        public string JobWorkName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? Unit { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Rate must be a positive value.")]
        public decimal Rate { get; set; }

        public bool IsActive { get; set; }
    }

    public class JobWorkResponseDto
    {
        public int JobWorkId { get; set; }
        public string JobWorkName { get; set; } = string.Empty;
        public string? Unit { get; set; }
        public decimal Rate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}