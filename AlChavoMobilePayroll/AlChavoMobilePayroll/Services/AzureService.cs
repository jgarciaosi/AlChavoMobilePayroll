using Azure;
using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
using System.IO;
using System.Threading.Tasks;

namespace AlChavoMobilePayroll.Services
{
    public class AzureService
    {


        public async Task UploadfileFromStream(Stream ParamFileStream, string ParamFileBaseName, string ParamExtension, string ParamCompid)
        {
            //var service = new FileManagerService();
          //  var service = null;
          //  var connectionString = await service.GetSasKey().ConfigureAwait(false);//"DefaultEndpointsProtocol=https;AccountName=osiproductionstorage;AccountKey=aQyWABGYGunnA658NIfrP8tGKrCsQ1gvelUVb8fce011K0g+J5yz9qXExx74COO7D/+WFV8hwW/P6/kFTY+BsQ==;EndpointSuffix=core.windows.net";
         //   var StorageDrive = await service.GetStorageDriveShareName(ParamCompid).ConfigureAwait(false);
         //   var ParamAzureStorageDirectory = await service.GetImagesFilePath(ParamCompid).ConfigureAwait(false); //"Image2/ImageTemp/Compid/Images/".Replace("Compid", ParamCompid);

            //string connectionString = Info.SasTkn;
            //string StorageDrive = Info.CompanyFileDrive;
            //string ParamAzureStorageDirectory = Info.CompanyImageFilePath;


            // Get a reference to a share and then create it
            //ShareClient share = new ShareClient(connectionString.SasKey, StorageDrive.StorageDriveShareName);
            //await share.CreateIfNotExistsAsync();

            //// Get a reference to a directory and create it
            //ShareDirectoryClient directory = share.GetDirectoryClient(ParamAzureStorageDirectory.ImagesFilePath);
            //await directory.CreateIfNotExistsAsync();

            // Get a reference to a file and upload it
            //ShareFileClient file = directory.GetFileClient(ParamFileBaseName + ParamExtension);

            const int blockSize = 4194304;
            long offset = 0;//Define http range offset
            BinaryReader reader = new BinaryReader(ParamFileStream);

            //file.Create(ParamFileStream.Length);
            //file.GetProperties();

            int bytesRead;
            long index = 0;
            byte[] buffer = new byte[blockSize];

            while ((bytesRead = ParamFileStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                // Create a memory stream for the buffer to upload
                using (MemoryStream ms = new MemoryStream(buffer, 0, bytesRead))
                {
                //    await file.UploadRangeAsync(ShareFileRangeWriteType.Update, new HttpRange(index, ms.Length), ms);
                    index += ms.Length; // increment the index to the account for bytes already written
                }
            }

            //while (offset < ParamFileStream.Length)
            //{
            //    byte[] buffer = reader.ReadBytes(blockSize);
            //    if (buffer.Length == 0)
            //        break;

            //    MemoryStream uploadChunk = new MemoryStream();
            //    uploadChunk.Write(buffer, 0, buffer.Length);
            //    uploadChunk.Position = 0;

            //    HttpRange httpRange = new HttpRange(offset, buffer.Length);
            //    await file.UploadRangeAsync(httpRange, uploadChunk);
            //    offset += buffer.Length;//Shift the offset by number of bytes already written
            //}
            //using (ParamFileStream)
            //{

            //    file.UploadRange(new HttpRange(0, ParamFileStream.Length), ParamFileStream);
            //}
        }
    }
}
