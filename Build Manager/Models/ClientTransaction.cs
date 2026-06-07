namespace BuildManager.Models
{
    /// <summary>
    /// Records money received from or paid to a client.
    /// Corresponds to the "Client Transaction Details" screen.
    /// Shows TxnDate, ClientName, CreditAmount, DebitAmount, Remarks.
    /// </summary>
    public class ClientTransaction
    {
        public int ClientTransactionId { get; set; }

        public DateTime TransactionDate { get; set; }

        public int ClientId { get; set; }

        public decimal CreditAmount { get; set; } = 0;

        public decimal DebitAmount { get; set; } = 0;

        public int? PaymentTypeId { get; set; }

        public int? ByWhomId { get; set; }

        public string? Remarks { get; set; }

        // Navigation properties
        public Client Client { get; set; } = null!;
        public PaymentType? PaymentType { get; set; }
        public Whom? ByWhom { get; set; }
    }
}
