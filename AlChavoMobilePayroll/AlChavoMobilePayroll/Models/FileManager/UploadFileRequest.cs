using System;
using System.Collections.Generic;
using System.Text;

namespace AlChavoMobilePayroll.Models.FileManager
{
    public class UploadFileRequest
    {
        public string CompId { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
    }

}
