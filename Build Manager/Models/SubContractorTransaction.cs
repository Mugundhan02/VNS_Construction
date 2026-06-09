using System.ComponentModel.DataAnnotations;

namespace BuildManager.Models
{
    public class SubContractorTransaction
    {
        [Key]
        public int SubContractorTransactionId { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        public int ClientId        { get; set; }
        public int SubContractorId { get; set; }
        public int JobWorkId       { get; set; }

        [Range(0, double.MaxValue)] public decimal Quantity   { get; set; } = 0;
        [MaxLength(50)]             public string? Unit       { get; set; }
        [Range(0, double.MaxValue)] public decimal Rate       { get; set; } = 0;
        [Range(0, double.MaxValue)] public decimal Amount     { get; set; } = 0;
        [Range(0, double.MaxValue)] public decimal PaidAmount { get; set; } = 0;

        public int?    PaymentTypeId { get; set; }
        public int?    ToWhomId      { get; set; }
        public string? Remarks       { get; set; }
        public bool    IsSubBill     { get; set; } = false;

        public Client         Client         { get; set; } = null!;
        public SubContractor  SubContractor  { get; set; } = null!;
        public JobWork        JobWork        { get; set; } = null!;
        public PaymentType?   PaymentType    { get; set; }
        public Whom?          ToWhom         { get; set; }
    }
}
