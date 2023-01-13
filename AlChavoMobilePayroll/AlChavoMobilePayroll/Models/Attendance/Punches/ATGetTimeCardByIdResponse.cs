using AlChavoMobilePayroll.Models.SManagement.Company;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlChavoMobilePayroll.Models.Attendance.Punches
{
    public class ATGetTimeCardByIdResponse
    {
        public string EmpID { get; set; }
        public DateTime? OrigPunchInDate { get; set; }
        public string EmployeeName { get; set; }
        public int? PunchDay { get; set; }
        public DateTime? PunchInDate { get; set; }
        public DateTime? PunchInHour { get; set; }
        public DateTime? PunchOutDate { get; set; }
        public DateTime? PunchOutHour { get; set; }
        public object HourType { get; set; }
        public float? SumOfHoursWorked { get; set; }
        public float? SumOfRegularHours { get; set; }
        public float? SumOfMealPenaltyHours { get; set; }
        public float? SumOfSickHours { get; set; }
        public float? SumOfVacHours { get; set; }
        public float? SumOfOverTime40PlusHours { get; set; }
        public float? SumOfOverTimeExces8Hour { get; set; }
        public float? SumOfOverTime24Hours { get; set; }
        public float? SumOfOverTime12HoursFlexiTime { get; set; }
        public float? DoubleTime { get; set; }
        public float? SumOfSixConsecutiveDays { get; set; }
        public float? SumOfSevenConsecutiveDays { get; set; }
        public float? SumOfSun_Reg { get; set; }
        public float? SumOfSundayOT { get; set; }
        public float? SumOfOtherHours { get; set; }
        public string CompId { get; set; }
        public object TimeCardID { get; set; }
        public object IsEditedPunchIn { get; set; }
        public string Department { get; set; }
        public string DptOut { get; set; }
        public float? Holidays { get; set; }
        public object IsEditedPunchOut { get; set; }
        public DateTime? OrigPunchInHour { get; set; }
        public DateTime? OrigPunchOutHour { get; set; }
        public object PunchInTC { get; set; }
        public object PunchOutTC { get; set; }
        public int? PunchInID { get; set; }
        public int? PunchOutID { get; set; }
        public DateTime? OrigPunchInDateOrder { get; set; }
        public float? SumOfOvertimeWeekly2 { get; set; }
        public float? SumOfDayOff { get; set; }
        public float? SumOfIncentiveShift { get; set; }
        public object Rate { get; set; }
        public bool IsVisibleHeader { get; set; }
        public bool IsPunchInFlagForEdit { get; set; }
       public List<DepartmentByCompaniesResponse> departmentList { get; set; }
}

    }

