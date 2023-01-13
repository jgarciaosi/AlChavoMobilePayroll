using AlChavoMobilePayroll.ExtensionMethods;
using AlChavoMobilePayroll.Helpers;
using AlChavoMobilePayroll.Models; 
using AlChavoMobilePayroll.Models.Attendance.Punches;
using AlChavoMobilePayroll.Models.SManagement.Company;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;

namespace AlChavoMobilePayroll.Services
{
    public class PunchService : BaseService
    {

        const string Module = "Attendance";
        // private readonly string apiUrl = ConfigurationManager.AppSettings["ApiKey"];
        private readonly string apiUrl = GetAPIKEY("ApiPAKey");
     
       
        
        public async Task<List<DepartmentByCompaniesResponse>> GetDepartmentByCompId(string CompId)
        {
            var url = $"{apiUrl}{Module}/{nameof(GetDepartmentByCompId)}";
            var uri = new UriBuilder(url);
            uri.AddQuery("Compid", CompId);
          
            var service = new HttpHelper<List<DepartmentByCompaniesResponse>>();
            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);
            return data;
        }

        public async Task<List<RecentPunchesResponse>> ATGetSSPRecentPunches(string CompId, string EmpId)
        {

            var url = $"{apiUrl}{Module}/{nameof(ATGetSSPRecentPunches)}";
            var uri = new UriBuilder(url);
            uri.AddQuery(Constants.CompIdParameter, CompId);
            uri.AddQuery(Constants.EmpIdParameter, EmpId);

            var service = new HttpHelper<List<RecentPunchesResponse>>();
            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);
            return data;
        }
        public async Task<List<ATGetAppSetupDefsResponse>> ATGetAppSetupDefs(string CompId)
        {

            var url = $"{apiUrl}{Module}/{nameof(ATGetAppSetupDefs)}";
            var uri = new UriBuilder(url).AddQuery(Constants.CompIdParameter, CompId);

            var service = new HttpHelper<List<ATGetAppSetupDefsResponse>>();
            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);
            return data;
        }
        public async Task<List<ATGetMobileLocationsResponse>> ATGetMobileLocations(string CompId, string EmpEntryNum, string EmpId)
        {

            var url = $"{apiUrl}{Module}/{nameof(ATGetMobileLocations)}";
            var uri = new UriBuilder(url);

            uri.AddQuery(Constants.CompIdParameter, CompId);
            uri.AddQuery(Constants.EmployeeEntryNum, EmpEntryNum);
            uri.AddQuery(Constants.EmpIdParameter, EmpId);

            var service = new HttpHelper<List<ATGetMobileLocationsResponse>>();
            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);
            return data;
        }
        public async Task<int> ATCheckPayrollStart(string CompId)
        {

            var url = $"{apiUrl}{Module}/{nameof(ATCheckPayrollStart)}";
            var uri = new UriBuilder(url).AddQuery(Constants.CompIdParameter, CompId);

            var service = new HttpHelper<int>();
            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);
            return data;
        }
        public async Task<List<ATGetTimeCardByDateSpanResponse>> ATGetTimeCardByDateSpan(string CompId, DateTime StartDate, DateTime EndDate, string EntryNum)
        {

            var url = $"{apiUrl}{Module}/{nameof(ATGetTimeCardByDateSpan)}";
            var uri = new UriBuilder(url);

            uri.AddQuery(Constants.CompIdParameter, CompId);
            uri.AddQuery(Constants.StartDateParameter, StartDate.ToString("yyyy-MM-dd"));
            uri.AddQuery(Constants.EndDateParameter, EndDate.ToString("yyyy-MM-dd"));
            uri.AddQuery(Constants.EntryNumParameter, EntryNum.ToString());

            var service = new HttpHelper<List<ATGetTimeCardByDateSpanResponse>>();
            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);
            return data;
        }
        public async Task<List<ATGetLastTimeCardByEmpEntryNumResponse>> ATGetLastTimeCardByEmpEntryNum(string CompId, string EmployeeEntryNum)
        {

            var url = $"{apiUrl}{Module}/{nameof(ATGetLastTimeCardByEmpEntryNum)}";
            var uri = new UriBuilder(url);

            uri.AddQuery(Constants.CompIdParameter, CompId);
            uri.AddQuery(Constants.EmployeeEntryNum, EmployeeEntryNum);

            var service = new HttpHelper<List<ATGetLastTimeCardByEmpEntryNumResponse>>();
            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);
            return data;
        }
        public async Task<int> ATCreateTimeCard(string CompId, string EmpId, string EmployeeEntryNum)
        {

            var url = $"{apiUrl}{Module}/{nameof(ATCreateTimeCard)}";
            var uri = new UriBuilder(url);

            uri.AddQuery(Constants.CompIdParameter, CompId);
            uri.AddQuery(Constants.EmpIdParameter, EmpId);
            uri.AddQuery(Constants.EmployeeEntryNum, EmployeeEntryNum);

            var service = new HttpHelper<int>();
            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);
            return data;
        }
        public async Task<List<ATGetEmployeeAutoPunchResponse>> ATGetEmployeeAutoPunch(string CompId, string EmpId, string EmployeeEntryNum, DateTime PunchTime)
        {

            var url = $"{apiUrl}{Module}/{nameof(ATGetEmployeeAutoPunch)}";
            var uri = new UriBuilder(url);

            uri.AddQuery(Constants.CompIdParameter, CompId);
            uri.AddQuery(Constants.EmpIdParameter, EmpId);
            uri.AddQuery(Constants.EmployeeEntryNum, EmployeeEntryNum);
            uri.AddQuery(Constants.PunchTimeParameter, PunchTime.ToString("yyyy-MM-dd"));

            var service = new HttpHelper<List<ATGetEmployeeAutoPunchResponse>>();
            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);
            return data;
        }
        public async Task<Response> ATPunch(PunchData punchData)
        {
            var url = $"{apiUrl}{Module}/{nameof(ATPunch)}";

            var service = new HttpHelper<Response>();
            var data = await service.PostRestServiceDataAsync(url, punchData).ConfigureAwait(false);
            return data;
        }

        public async Task<Response> ATPunchEdit(PunchData punchData)
        {
            var url = $"{apiUrl}{Module}/{nameof(ATPunchEdit)}";

            var service = new HttpHelper<Response>();
            var data = await service.PostRestServiceDataAsync(url, punchData).ConfigureAwait(false);
            return data;
        }
        public async Task<Response> ATAutoPunch(PunchData punchData)
        {
            var url = $"{apiUrl}{Module}/{nameof(ATAutoPunch)}";

            var service = new HttpHelper<Response>();
            var data = await service.PostRestServiceDataAsync(url, punchData).ConfigureAwait(false);
            return data;
        }

        public async Task<Response> ATUpdateMobilePunches(PunchData punchData)
        {
            var url = $"{apiUrl}{Module}/{nameof(ATUpdateMobilePunches)}";

            var service = new HttpHelper<Response>();
            var data = await service.PostRestServiceDataAsync(url, punchData).ConfigureAwait(false);
            return data;
        }

        public async Task<Response> ATDeletePunchByID(PunchData punchData)
        {
            var url = $"{apiUrl}{Module}/{nameof(ATDeletePunchByID)}";
            
            var service = new HttpHelper<Response>();
            
            var data = await service.PostRestServiceDataAsync(url, punchData).ConfigureAwait(false);

            return data;
        }

        public async Task<Response> ATCheckNotifications(CheckNotifications aTCheckNotifications)
        {

            var url = $"{apiUrl}{Module}/{nameof(ATCheckNotifications)}";


            var service = new HttpHelper<Response>();
            var data = await service.PostRestServiceDataAsync(url, aTCheckNotifications).ConfigureAwait(false);
            return data;
        }
    }

    
    public class Response
    {
        public String Status { get; set; }
        public String StatusMessage { get; set; }


    }
}
