namespace BuildManager.Models
{
    /// <summary>
    /// Lookup table for payment types.
    /// Corresponds to the "Company Payment Details" screen.
    /// Example values: Cash, Cheque, GPAY, NEFT, Phone Pay, RTGS.
    /// </summary>
    public class PaymentType
    {
        public int PaymentTypeId { get; set; }

        public string PaymentTypeName { get; set; } = string.Empty;
    }
}
