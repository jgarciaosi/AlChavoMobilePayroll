using AlChavoMobilePayroll.Models.FileManager;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AlChavoMobilePayroll.Helpers
{
    public class HttpHelper<T>

    {

        public async Task<T> GetRestServiceDataAsync(string serviceAddress)
        {try { 
            var AuthToken = await GetUserSessionToken();
            var client = new HttpClient();

            client.BaseAddress = new Uri(serviceAddress);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AuthToken);

            var response = await client.GetAsync(client.BaseAddress);
            response.EnsureSuccessStatusCode();

            var jsonresult = await response.Content.ReadAsStringAsync();
            
            
   var result = JsonConvert.DeserializeObject<T>(jsonresult);
            
              return result;
            }
            catch(Exception ex) { 
                var s = ex.Message; 
            }

            var AuthToken4 = await GetUserSessionToken();
            var client4 = new HttpClient();

            client4.BaseAddress = new Uri(serviceAddress);
            client4.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AuthToken4);

            var response4 = await client4.GetAsync(client4.BaseAddress);
            response4.EnsureSuccessStatusCode();

            var jsonresult4 = await response4.Content.ReadAsStringAsync();


            var result4 = JsonConvert.DeserializeObject<T>(jsonresult4);

            return result4;


        }


        public async Task<T> PostRestServiceDataAsync(String url, object Model)
        {
            try { 

            var AuthToken = await GetUserSessionToken();
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AuthToken);

            var json = JsonConvert.SerializeObject(Model);

            var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(client.BaseAddress, contentJson);
            response.EnsureSuccessStatusCode();

            
            var jsonresult = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(jsonresult);

            return result;

            }
            catch (Exception ex)
            {
                var s = ex.Message;
            }

            var AuthToken4 = await GetUserSessionToken();
            var client4 = new HttpClient();
            client4.BaseAddress = new Uri(url);
            client4.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AuthToken4);

            var json4 = JsonConvert.SerializeObject(Model);

            var contentJson4 = new StringContent(json4, Encoding.UTF8, "application/json");
            var response4 = await client4.PostAsync(client4.BaseAddress, contentJson4);
            response4.EnsureSuccessStatusCode();


            var jsonresult4 = await response4.Content.ReadAsStringAsync();

            var result4 = JsonConvert.DeserializeObject<T>(jsonresult4);

            return result4;

        }
        public async Task<HttpResponseMessage> PostRestServiceFilesAsync(String url, List<FileTransferModel> FileList)
        {
            var AuthToken = await GetUserSessionToken();
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AuthToken);


            var content = new MultipartFormDataContent();
            foreach (var File in FileList)
            {
                content.Add(File.FileStreamContent, File.FileName, File.FileName);
            }
         
            var response = await client.PostAsync(client.BaseAddress, content);

            return response;
        }

        public async Task<T> PutRestServiceDataAsync(String url, object Model)
        {
            var AuthToken = await GetUserSessionToken();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AuthToken);

            Uri RequesUri = new Uri(url);

            var json = JsonConvert.SerializeObject(Model);

            var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(RequesUri, contentJson);
            response.EnsureSuccessStatusCode();

            var jsonresult = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(jsonresult);

            return result;
        }

        public async Task<T> PostLoginRestServiceDataAsync(String url, object Model)
        {
            var client = new HttpClient();
            Uri RequesUri = new Uri(url);

            var json = JsonConvert.SerializeObject(Model);

            var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(RequesUri, contentJson);
            response.EnsureSuccessStatusCode();

            var jsonresult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(jsonresult);

            return result;
        }

        private async Task<string> GetUserSessionToken()
        {
            return await SecureStorage.GetAsync("AuthTkn");
        }
    }
}
