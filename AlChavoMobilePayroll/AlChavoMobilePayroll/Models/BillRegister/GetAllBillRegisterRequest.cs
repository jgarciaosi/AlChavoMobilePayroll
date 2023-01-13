using System;

namespace AlChavoMobilePayroll.Models.BillRegister
{
    public class GetAllBillRegisterRequest
    {
        public string CompId { get; set; }
        public string PayeeId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int IncludeVoid { get; set; }
        public string UserGuid { get; set; }
        public string Lang { get; set; }
    }
}
