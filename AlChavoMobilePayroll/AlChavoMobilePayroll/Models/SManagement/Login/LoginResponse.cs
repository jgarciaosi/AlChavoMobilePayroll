namespace AlChavoMobilePayroll.Models.SManagement.Login
{
    public class LoginResponse
    {
        public string EmpId { get; set; }
        public int EmployeeEntryNum { get; set; }
        public string UserName { get; set; }
        public string AC30UserID { get; set; }
        public string AC20UserID { get; set; }
        public string FullName { get; set; }
        public string Token { get; set; }
        public string LoginStatus { get; set; }
        public string CompId    { get; set; }
        public int PAMobileOptionalFieldsYN { get; set; }
        public int PAMobileEditPunchYN { get; set; }
        public int PAMobileCanPunchYN { get; set; }
        public string DepartmentId { get; set; }

    }
}
