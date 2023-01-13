using System;

namespace AlChavoMobilePayroll.Models.BillRegister
{
    public class GetAllBillRegisterResponse
    {
        public string CompId { get; set; }
        public long? TransId { get; set; }
        public string PayeeId { get; set; }
        public string PayeeInfo { get; set; }
        public string InvoiceNum { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public DateTime? NetDueDate { get; set; }
        public DateTime? DiscDueDate { get; set; }
        public decimal? InvoiceAmt { get; set; }
        public decimal? PmtAmtApplied { get; set; }
        public decimal? BalanceDue { get; set; }
        public int? VoidYN { get; set; }
        public long? TaskId { get; set; }
        public string ViewBill { get; set; }
        public string Status { get; set; }
        public int? JournalEntry { get; set; }
        public bool? IsRecurrent { get; set; }
        public bool? isPostedBill { get; set; }
        public string SeparatedCheckNumber { get; set; }
        public int? InvoicePaymentCount { get; set; }
        public bool? IsRecurrentExpired { get; set; }
        public int? RecurrentId { get; set; }
        public int? DeliveryInstructions { get; set; }
    }
}
