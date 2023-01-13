using System;
using System.Collections.Generic;
using System.Text;

namespace AlChavoMobilePayroll.Models.Attendance.Punches
{
    public class ATGetPayrollTimecardsPeriodResponse
    {
        public int ID { get; set; }
        public string CompID { get; set; }
        public DateTime PayrollStartPeriod { get; set; }
        public DateTime PayrollEndPeriod { get; set; }
        public string Freq { get; set; }
        public string PeriodDescription { get; set; }

        public static implicit operator ATGetPayrollTimecardsPeriodResponse(string v)
        {
            throw new NotImplementedException();
        }
    }

}
