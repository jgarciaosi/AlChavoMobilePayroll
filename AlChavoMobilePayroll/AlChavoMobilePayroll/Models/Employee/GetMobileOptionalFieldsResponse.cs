using System;
using System.Collections.Generic;
using System.Text;
using Telerik.Windows.Documents.Flow.Model.Fields;

namespace AlChavoMobilePayroll.Models.Employee
{
    public class GetMobileOptionalFieldsResponse
    {
        public int DocumentCategoryID { get; set; }
        public string Field { get; set; }

        public string FileName { get; set; }
    }
}
