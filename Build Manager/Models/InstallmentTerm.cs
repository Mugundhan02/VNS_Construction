using System.ComponentModel.DataAnnotations;

namespace BuildManager.Models
{
    public class InstallmentTerm
    {
        [Key]
        public int InstallmentTermId { get; set; }

        [Required, MaxLength(200)]
        public string TermName { get; set; } = string.Empty;

        [Range(1, int.MaxValue)]
        public int? NumberOfInstallments { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }
    }
}
