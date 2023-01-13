using System;

namespace AlChavoMobilePayroll.Models.Bill
{
    public class GetPaymentTransactionsResponse
    {
        public string PayeeId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal GrossAmt { get; set; }
        public decimal ProfServAmt { get; set; }
        public decimal DiscAmt { get; set; }
        public decimal TaxAmt { get; set; }
        public decimal NetAmt { get; set; }
        public long TransId { get; set; }
    }
}
