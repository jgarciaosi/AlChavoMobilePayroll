using System;
using System.Collections.Generic;
using System.Text;

namespace AlChavoMobilePayroll.Models.Attendance.Punches
{
     public class ATGetLastPunchResponse
    {
        public int ID { get; set; }
        public string PunchHour { get; set; }
        public DateTime PunchDate { get; set; }
        public bool isPunchIn { get; set; }

        public bool isPunchOut { get; set; }
    }

}
