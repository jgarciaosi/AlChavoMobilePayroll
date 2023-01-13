using System;
using System.Collections.Generic;
using System.Text;

namespace AlChavoMobilePayroll.Models.CheckRegister
{
    public class GetCheckRegisterDetailYTDTotalsResponse
    {
        public float CurrenDeductions { get; set; }
        public float Gross { get; set; }
        public float Net { get; set; }
        public float YTDGross { get; set; }
        public float YTDEarn { get; set; }
        public float YTDDed { get; set; }


    }
}
