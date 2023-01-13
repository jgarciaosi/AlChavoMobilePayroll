using System;

namespace AlChavoMobilePayroll.Models.SManagement.Company
{
    public class DepartmentByCompaniesResponse
    {
        public Int32 EntryNum { get; set; }
        public string DepartmentId { get; set; }
        public string Description { get; set; }                
        public int PunchSelected { get; set; }        
    }
}
