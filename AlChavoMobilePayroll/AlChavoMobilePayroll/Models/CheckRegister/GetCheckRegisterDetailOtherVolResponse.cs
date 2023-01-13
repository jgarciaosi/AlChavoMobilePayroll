using System;
using System.Collections.Generic;
using System.Text;

namespace AlChavoMobilePayroll.Models.CheckRegister
{
    public class GetCheckRegisterDetailOtherVolResponse
    {
        public string CompId { get; set; }
        public string EmpId { get; set; }
        public string OtherVolWithholdId { get; set; }
        public string OtherVolWithholdDesc { get; set; }
        public float AmtOtherVol { get; set; }

    }
}
