using System.ComponentModel.DataAnnotations;

namespace BuildManager.Models
{
    public class PaymentType
    {
        [Key]
        public int PaymentTypeId { get; set; }

        [Required, MaxLength(100)]
        public string PaymentTypeName { get; set; } = string.Empty;
    }
}
