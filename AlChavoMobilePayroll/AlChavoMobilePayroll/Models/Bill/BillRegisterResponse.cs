using System;
using System.Collections.Generic;
using System.Text;

namespace AlChavoMobilePayroll.Models.Bill
{
    public class BillRegisterResponse
    {

        public string CompId { get; set; }
        public int TransId { get; set; }
        public string PayeeId { get; set; }
        public string PayeeInfo { get; set; }
        public string InvoiceNum { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime NetDueDate { get; set; }
        public DateTime DiscDueDate { get; set; }
        public float InvoiceAmt { get; set; }
        public float PmtAmtApplied { get; set; }
        public float BalanceDue { get; set; }
        public int VoidYN { get; set; }
        public object TaskId { get; set; }
        public string ViewBill { get; set; }
        public string Status { get; set; }
        public int JournalEntry { get; set; }
        public bool IsRecurrent { get; set; }
        public bool isPostedBill { get; set; }
        public string SeparatedCheckNumber { get; set; }
        public int InvoicePaymentCount { get; set; }
        public object IsRecurrentExpired { get; set; }
        public object RecurrentId { get; set; }
    }

}
