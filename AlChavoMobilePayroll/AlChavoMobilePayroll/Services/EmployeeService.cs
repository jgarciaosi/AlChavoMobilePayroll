using AlChavoMobilePayroll.ExtensionMethods;
using AlChavoMobilePayroll.Helpers;
using AlChavoMobilePayroll.Models.Attendance;
using AlChavoMobilePayroll.Models.Employee;
using AlChavoMobilePayroll.ViewModels;
using AlChavoMobilePayroll.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlChavoMobilePayroll.Services
{
    public class EmployeeService:AlChavoMobilePayroll.Services.BaseService
    {
        const string Module = "Employee";
        //private readonly string apiUrl = ConfigurationManager.AppSettings["ApiKey"]; // = https://osialchavoapi.azurewebsites.net/api/
        //private readonly string Ac30ApiUrl = ConfigurationManager.AppSettings["AC30ApiKey"]; // = https://osialchavoapi.azurewebsites.net/api/


        // private readonly string apiUrl = ConfigurationManager.AppSettings["ApiKey"];
        private string apiUrl = GetAPIKEY("ApiPAKey");

        public async Task<List<GetMobileOptionalFieldsResponse>> PAGetMobileOptionalFields(string CompId,string EmpId)
        {
            var url = $"{apiUrl}{Module}/{nameof(PAGetMobileOptionalFields)}";
            var uri = new UriBuilder(url);
            uri.AddQuery("CompID", CompId);
            uri.AddQuery("EmpId", EmpId);            

            var service = new HttpHelper<List<GetMobileOptionalFieldsResponse>>();

            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);
                        
            return data;
        }

        public async Task<GetMobileEmployeeFullInfoResponse> PAGetMobileEmployeeFullInfo(string CompId, string EmpId)
        {
            var url = $"{apiUrl}{Module}/{nameof(PAGetMobileEmployeeFullInfo)}";
            var uri = new UriBuilder(url);
            uri.AddQuery("CompID", CompId);
            uri.AddQuery("EmpId", EmpId);

            var service = new HttpHelper<GetMobileEmployeeFullInfoResponse>();

            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);

            return data;
        }
    }
}
