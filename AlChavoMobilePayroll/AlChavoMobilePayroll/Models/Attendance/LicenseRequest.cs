using System;

namespace AlChavoMobilePayroll.Models.Attendance
{
    public class LicenseRequest
    {
        public string CompId { get; set; }
        public string EmpId { get; set; }
        public int EmployeeEntryNum { get; set; }
        public string DepartmentID { get; set; }
        public DateTime RequestDateFrom { get; set; }
        public DateTime RequestDateTo { get; set; }
        public double RequestHours { get; set; }
        public string RequestLicenseEarning { get; set; }
        public string RequestComments { get; set; }
        public DateTime RequestDateRequested { get; set; }
    }
}
