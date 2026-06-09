using System.ComponentModel.DataAnnotations;

namespace BuildManager.Models
{
    public class ClientTransaction
    {
        [Key]
        public int ClientTransactionId { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        public int ClientId { get; set; }

        [Range(0, double.MaxValue)]
        public decimal CreditAmount { get; set; } = 0;

        [Range(0, double.MaxValue)]
        public decimal DebitAmount { get; set; } = 0;

        public int?    PaymentTypeId { get; set; }
        public int?    ByWhomId      { get; set; }
        public string? Remarks       { get; set; }

        public Client       Client      { get; set; } = null!;
        public PaymentType? PaymentType { get; set; }
        public Whom?        ByWhom      { get; set; }
    }
}
