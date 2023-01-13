using System;
using System.Collections.Generic;
using System.Text;

namespace AlChavoMobilePayroll.Models.Attendance.Punches
{
    public class ATGetAppSetupDefsResponse
    {
        public string NAME { get; set; }
        public string Description { get; set; }
        public Nullable<Double> ValueNumeric { get; set; }
        public string ValueAlpha { get; set; }
        public bool Isnumeric { get; set; }
    }
}
