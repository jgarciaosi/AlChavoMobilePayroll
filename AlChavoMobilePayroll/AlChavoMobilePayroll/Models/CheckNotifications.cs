using System;
using System.Collections.Generic;
using System.Text;

namespace AlChavoMobilePayroll.Models
{
    public class CheckNotifications
    {
       public string ArrivalNot { get; set; }
       public string LateNot { get; set; }
       public int DepartmentEntryNum { get; set; }
       public int EmployeeEntryNum { get; set; }
       public string CompId { get; set; }
       public DateTime PunchTime { get; set; }
       public string EmployeeName { get; set; }
       public string UserGUID { get; set; }
       public string AbsentNOT { get; set; }
       public string MissingNOT { get; set; }
       public string EarlyArrival { get; set; }
       public string EmpId { get; set; }
    }
}
