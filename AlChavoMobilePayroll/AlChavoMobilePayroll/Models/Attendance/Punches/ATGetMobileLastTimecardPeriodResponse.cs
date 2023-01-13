using System;
using System.Collections.Generic;
using System.Text;

namespace AlChavoMobilePayroll.Models.Attendance.Punches
{

    public class ATGetMobileLastTimecardPeriodResponse
    {
        public int ID { get; set; }
        public DateTime PayrollStartPeriod { get; set; }
        public DateTime PayrollEndPeriod { get; set; }
    }

}

