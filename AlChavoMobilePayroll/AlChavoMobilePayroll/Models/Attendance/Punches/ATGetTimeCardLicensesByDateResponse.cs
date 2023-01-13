using System;
using System.Collections.Generic;
using System.Text;

namespace AlChavoMobilePayroll.Models.Attendance.Punches
{
    public class ATGetTimeCardLicensesByDateResponse
    {
            public string EmployeeID { get; set; }
            public DateTime? LicenseStart { get; set; }
            public DateTime? LicenseEnd { get; set; }
            public float RequestedHours { get; set; }
            public string EarningID { get; set; }
            public string LicenseStatus { get; set; }
            public string Comments { get; set; }
            public DateTime? ApprovedDate { get; set; }
        

    }
}
