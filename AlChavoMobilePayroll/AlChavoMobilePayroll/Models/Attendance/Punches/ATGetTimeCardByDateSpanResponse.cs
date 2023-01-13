using System;
using System.Collections.Generic;
using System.Text;

namespace AlChavoMobilePayroll.Models.Attendance.Punches
{
    public class ATGetTimeCardByDateSpanResponse
    {
        public DateTime carddate { get; set; }
        public bool approved { get; set; }
        public string ApprovedBy { get; set; }
        public string nombre { get; set; }
    }
}
