using System;
using System.Collections.Generic;
using System.Text;

namespace AlChavoMobilePayroll.Models.Attendance
{
    public class GetLicenseByStatusRequest
    { 
        public string CompID { get; set; }
        public string EmployeeID { get; set; }
        public bool Processed { get; set; }

    }
}
