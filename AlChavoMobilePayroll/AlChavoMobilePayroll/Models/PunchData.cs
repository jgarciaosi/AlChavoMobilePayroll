using System;
using System.Collections.Generic;
using System.Text;

namespace AlChavoMobilePayroll.Models
{
    public class PunchData
    {
        public int PunchID { get; set; }
        public string CompId { get; set; }
        public string EmpId { get; set; }
        public Int32 EmployeeEntryNum { get; set; }
        public DateTime PunchTime { get; set; }
        public DateTime CardDate { get; set; }
        public string DepartmentId { get; set; }
        public string PunchLocation { get; set; }
        public Int32 PunchType { get; set; }
        public Boolean IsPunchIn { get; set; }
        public string UserGuid { get; set; }
        public string Photo { get; set; }
        public Double Lat { get; set; }
        public Double Long { get; set; }
        public string IP { get; set; }
        public string PunchTimeString { get; set; }
    }
}
