using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlChavoMobilePayroll.DepServices
{
  public  interface IFileService
    {
        string storageDirectory { get; }
      
         void Download(string url, string folder,string fileName = "");
      
     
        event EventHandler<DownloadEventArgs> OnFileDowloaded;
        
        void OpenFile(string path);

        void SaveImageFromByte(byte[] imageByte, string filename);
    }
}
       



