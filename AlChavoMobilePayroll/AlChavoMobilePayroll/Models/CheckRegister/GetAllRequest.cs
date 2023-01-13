using System;

namespace AlChavoMobilePayroll.Models.CheckRegister
{
    public class GetAllRequest
    {
        public string Compid { get; set; }
        public string EmployeeId { get; set; }
        public DateTime CheckDateFrom { get; set; }
        public DateTime CheckDateTo { get; set; }
       
    }
}
