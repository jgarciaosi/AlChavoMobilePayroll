using System;
using System.Collections.Generic;
using System.Text;

namespace AlChavoMobilePayroll.Models.Attendance
{
    public class SchedulePeriodResponse
    {
            public string CompID { get; set; }
            public string EmployeeID { get; set; }
            public DateTime FromPeriod { get; set; }
            public DateTime ToPeriod { get; set; }
            public string PeriodDisplay { get; set; }




    }
}
