using System.ComponentModel.DataAnnotations;

namespace BuildManager.DTOs
{
    public class MaterialRequestDto
    {
        [Required, MaxLength(200)]
        public string MaterialName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? Unit { get; set; }

        public decimal Rate { get; set; } = 0;
    }

    public class MaterialResponseDto
    {
        public int    MaterialId   { get; set; }
        public string MaterialName { get; set; } = string.Empty;
        public string? Unit        { get; set; }
        public decimal Rate        { get; set; }
    }
}
