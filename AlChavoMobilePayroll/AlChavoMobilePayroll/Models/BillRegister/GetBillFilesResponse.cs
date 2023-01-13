using System;

namespace AlChavoMobilePayroll.Models.BillRegister
{
    public class GetBillFilesResponse
    {
        public string FileName { get; set; }
        public Uri FileUri { get; set; }
        public bool isImage { get; set; }
    }
}
