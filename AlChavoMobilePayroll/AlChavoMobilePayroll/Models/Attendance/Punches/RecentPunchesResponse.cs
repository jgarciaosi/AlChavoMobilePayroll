using System;
using System.Collections.Generic;
using System.Text;

namespace AlChavoMobilePayroll.Models.Attendance.Punches
{
    public class RecentPunchesResponse
    {
        public Int32 PunchID { get; set; }
        public DateTime PunchTime { get; set; }
        public Boolean isPunchIn { get; set; }
        public string PunchDesc { get; set; }
        public int DepartmentEntryNum { get; set; }
        public string DepartmentId { get; set; }
        

    }
}
