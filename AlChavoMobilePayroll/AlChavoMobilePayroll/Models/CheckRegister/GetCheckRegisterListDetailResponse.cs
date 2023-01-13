using System;
using System.Collections.Generic;
using System.Text;

namespace AlChavoMobilePayroll.Models.CheckRegister
{
    public class GetCheckRegisterListDetailResponse
    {
        public string CompId { get; set; }
        public string EmpId { get; set; }
        public string StateTaxId { get; set; }
        public string StateTaxDscr { get; set; }
        public float StateTaxAmt { get; set; }

    }
}
