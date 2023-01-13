using System;
using System.Collections.Generic;
using System.Text;

namespace AlChavoMobilePayroll.Models.CheckRegister
{
    public class GetCheckRegisterReportRequest
    {

        public string Compid { get; set; }
        public string EmployeeId { get; set; }
        public int PayrollSequence { get; set; }
        public DateTime CheckDate { get; set; }
        public string CheckNumber { get; set; }
        public string BankId { get; set; }
        public int NextCheckOrder { get; set; }

    }
}
