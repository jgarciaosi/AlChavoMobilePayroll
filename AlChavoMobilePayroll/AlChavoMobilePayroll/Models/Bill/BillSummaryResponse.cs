using System;
using System.Collections.Generic;
using System.Text;

namespace AlChavoMobilePayroll.Models.Bill
{
    public class BillSummaryResponse
    {
            public int? OpenBills { get; set; }
            public float? OpenBillsAmount { get; set; }
            public int? OverDueBills { get; set; }
            public float? OverDueBillsAmount { get; set; }
            public int? ScheduleBills { get; set; }
            public float? ScheduleBillsAmount { get; set; }
        }

    }


