using System.ComponentModel.DataAnnotations;

namespace BuildManager.Models
{
    public class JobWork
    {
        [Key]
        public int JobWorkId { get; set; }

        [Required, MaxLength(200)]
        public string JobWorkName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? Unit { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Rate { get; set; } = 0;

        public ICollection<SubContractorTransaction> SubContractorTransactions { get; set; } = new List<SubContractorTransaction>();
    }
}
