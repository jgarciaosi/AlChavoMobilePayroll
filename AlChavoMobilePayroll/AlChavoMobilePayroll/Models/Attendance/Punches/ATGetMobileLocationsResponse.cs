
using System;

namespace AlChavoMobilePayroll.Models.Attendance.Punches
{
    public class ATGetMobileLocationsResponse
    {
        public bool Enforced { get; set; }
        public Double Latitude { get; set; }
        public Double Longitude { get; set; }
        public string IP { get; set; }
        public string Device { get; set; }
    }
}
