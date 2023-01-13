using System;
using System.Collections.Generic;
using System.Text;

namespace AlChavoMobilePayroll.Models.Attendance
{
    public class ScheduleResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmployeeID { get; set; }
        public int DayOfWeek { get; set; }
        public Nullable<DateTime> HeaderPunchDate { get; set; }
        public DateTime FirstPunchDate { get; set; }
        public TimeSpan FirstPunchHour { get; set; }
        public DateTime SecondPunchDate { get; set; }
        public TimeSpan SecondPunchHour { get; set; }
        public bool IsVisibleHeader { get; set; }

        public double DurationTime { get; set; }
    }
}
