using System;
using System.Collections.Generic;

using System.Net.Http;
using System.Text;

namespace AlChavoMobilePayroll.Models.FileManager
{
    public class FileTransferModel

    {
        public StreamContent FileStreamContent { get; set; }
        public string FileName { get; set; }
        public string FileExt { get; set; }
    }
}
