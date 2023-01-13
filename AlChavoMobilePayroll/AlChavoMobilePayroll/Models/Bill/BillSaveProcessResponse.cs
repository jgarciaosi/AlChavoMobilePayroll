using System;
using System.Collections.Generic;
using System.Text;

namespace AlChavoMobilePayroll.Models.Bill
{
    public class BillSaveProcessResponse
    {
        public string Status { get; set; }
        public string StatusMessage { get; set; }
        public int TransId { get; set; }
        public string CheckFileURI { get; set; }
        public string CheckFileName { get; set; }
    }


}
