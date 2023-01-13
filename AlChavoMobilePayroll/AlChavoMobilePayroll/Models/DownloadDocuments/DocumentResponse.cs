using System;
using System.Collections.Generic;
using System.Text;

namespace AlChavoMobilePayroll.Models.DownloadDocuments
{
    public class DocumentResponse
    {
        public int DocumentCategoryID { get; set; }
        public string DocumentCategoryName { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }

    }
}
