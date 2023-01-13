using System;
using System.Collections.Generic;
using System.Text;

namespace AlChavoMobilePayroll.Models.Attendance
{
    public class GetLicenseByStatusResponse
    {
        public int ID { get; set; }
        public string CompID { get; set; }
        public int EmployeeEntryNum { get; set; }
        public string EmployeeID { get; set; }
        public string LicenseDate { get; set; }
        public string EarningID { get; set; }
        public float RequestedHours { get; set; }
        public bool Approved { get; set; }
        public bool Denied { get; set; }
        public string Status { get; set; }

    }
}
