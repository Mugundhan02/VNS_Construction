using System.ComponentModel.DataAnnotations;

namespace BuildManager.Models
{
    public class Material
    {
        [Key]
        public int MaterialId { get; set; }

        [Required, MaxLength(200)]
        public string MaterialName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? Unit { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Rate { get; set; } = 0;

        public ICollection<SupplierTransaction> SupplierTransactions { get; set; } = new List<SupplierTransaction>();
    }
}
