using System;
using System.Collections.Generic;
using System.Text;

namespace AlChavoMobilePayroll.Models.BillRegister
{
   public class GetBillPaymentsRequest
    {
        public string CompId { get; set; }
        public string Lang { get; set; }
        public string BankId { get; set; }
        public string PayeeId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int IncludeVoid { get; set; }
        public string CheckNumber { get; set; }
        public int TransId { get; set; }
    }
}
