using System;
using System.Collections.Generic;
using System.Text;

namespace AlChavoMobilePayroll.Models.Attendance.Punches
{
    public class ATGetLastTimeCardByEmpEntryNumResponse
    {
        public int ID { get; set; }
        public string EmployeeID { get; set; }
        public DateTime CardDate { get; set; }
        public bool Approved { get; set; }
        public string approvedby { get; set; }
        public string nombre { get; set; }
    }
}
