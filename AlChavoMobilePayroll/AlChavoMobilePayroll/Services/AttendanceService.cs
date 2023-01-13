using AlChavoMobilePayroll.ExtensionMethods;
using AlChavoMobilePayroll.Helpers;
using AlChavoMobilePayroll.Models.Attendance;
using AlChavoMobilePayroll.Models.Attendance.Punches;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace AlChavoMobilePayroll.Services
{
    public class AttendanceService : BaseService
    {
        const string Module = "Attendance";
        //private readonly string apiUrl = ConfigurationManager.AppSettings["ApiKey"]; // = https://osialchavoapi.azurewebsites.net/api/
        //private readonly string Ac30ApiUrl = ConfigurationManager.AppSettings["AC30ApiKey"]; // = https://osialchavoapi.azurewebsites.net/api/


        // private readonly string apiUrl = ConfigurationManager.AppSettings["ApiKey"];
        private string apiUrl = GetAPIKEY("ApiPAKey");

        public async Task<ObservableCollection<GetLicenseByStatusResponse>> ATGetSSPLicensesByStatus(GetLicenseByStatusRequest getLicenseByStatusRequest)
        {
            var url = $"{apiUrl}{Module}/{nameof(ATGetSSPLicensesByStatus)}";
            var uri = new UriBuilder(url);
            uri.AddQuery("CompID", getLicenseByStatusRequest.CompID);
            uri.AddQuery("EmployeeID", getLicenseByStatusRequest.EmployeeID);
            uri.AddQuery("Processed", getLicenseByStatusRequest.Processed.ToString());
         
            var service = new HttpHelper<ObservableCollection<GetLicenseByStatusResponse>>();

            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);

            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].Approved)
                {
                    data[i].Status = "✓ Approved";
                }
               
                if (data[i].Denied)
                {
                    data[i].Status = "✗ Denied";
                }
            }

            return data;
        }

        public async Task<ObservableCollection<GetLicenseByStatusResponse>> ATGetSSPLicensesByStatusPending(GetLicenseByStatusRequest getLicenseByStatusRequest)
        {
            var url = $"{apiUrl}{Module}/{nameof(ATGetSSPLicensesByStatus)}";
            var uri = new UriBuilder(url);
            uri.AddQuery("CompID", getLicenseByStatusRequest.CompID);
            uri.AddQuery("EmployeeID", getLicenseByStatusRequest.EmployeeID);
            uri.AddQuery("Processed", getLicenseByStatusRequest.Processed.ToString());

            var service = new HttpHelper<ObservableCollection<GetLicenseByStatusResponse>>();

            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);

            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].Approved)
                {
                    data[i].Status = "✓ Approved";
                }

                if (data[i].Denied)
                {
                    data[i].Status = "✗ Denied";
                }
            }

            return data;
        }

        public async Task<List<GetDefaultEarningsLicensesResponse>> ATGetDefaultEarningsLicenses(GetDefaultEarningsLicensesRequest getDefaultEarningsLicenseRequest)
        {
            var url = $"{apiUrl}{Module}/{nameof(ATGetDefaultEarningsLicenses)}";
            var uri = new UriBuilder(url);
            uri.AddQuery("Compid", getDefaultEarningsLicenseRequest.Compid);
            uri.AddQuery("Lang", getDefaultEarningsLicenseRequest.Lang);            

            var service = new HttpHelper<List<GetDefaultEarningsLicensesResponse>>();

            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);


            return data;
        }

        public async Task<GetRecordLicenseResponse> ATRecordLicense(LicenseRequest licenseRequest)
        {
            var url = $"{apiUrl}{Module}/{nameof(ATRecordLicense)}";
            var uri = new UriBuilder(url);           

            var service = new HttpHelper<GetRecordLicenseResponse>();

            var data = await service.PostRestServiceDataAsync(url,licenseRequest).ConfigureAwait(false);

            return data;
        }

        public async Task<GetRecordLicenseResponse> ATMobileDeleteLicense(String CompID, int IdLicenseParam)
        {
            var url = $"{apiUrl}{Module}/{nameof(ATMobileDeleteLicense)}";
            var uri = new UriBuilder(url);
            var model = new { CompId = CompID, IdLicense = IdLicenseParam };
            var service = new HttpHelper<GetRecordLicenseResponse>();

            var data = await service.PostRestServiceDataAsync(url, model).ConfigureAwait(false);

            return data;
        }

        public async Task<Uri> ATTimeOffMobileReport(string compId,string userName, string empId, DateTime dateFrom, DateTime dateTo)
        {
            apiUrl = GetAPIKEY("ApiPAKey");
            var url = $"{apiUrl}{Module}/{nameof(ATTimeOffMobileReport)}";
            var model = new { CompId = compId, UserName=userName,  EmpId = empId, DateFrom = dateFrom, DateTo = dateTo};
            var service = new HttpHelper<Uri>();
            var data = await service.PostRestServiceDataAsync(url, model).ConfigureAwait(false);
            return data;
        }

        public async Task<List<ATGetTimeCardByIdResponse>> ATGetMobileTimeCardByPeriodID(string compId,string empId,int periodId,string userId)
        {
            var url = $"{apiUrl}{Module}/{nameof(ATGetMobileTimeCardByPeriodID)}";
            var uri = new UriBuilder(url);
            uri.AddQuery("CompId", compId);
            uri.AddQuery("EmpId", empId);
            uri.AddQuery("PeriodID", periodId.ToString());
            uri.AddQuery("ParamUserID", userId);

            var service = new HttpHelper<List<ATGetTimeCardByIdResponse>>();

            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);
                       

            return data;
        }

        public async Task<ObservableCollection<ATGetTimeCardLicensesByDateResponse>> ATGetMobileTimecardLicenses(string compId, string empId, DateTime timecardStartDate, DateTime timecardEndDate)
        {
            var url = $"{apiUrl}{Module}/{nameof(ATGetMobileTimecardLicenses)}";
            var uri = new UriBuilder(url);
            uri.AddQuery("CompId", compId);
            uri.AddQuery("EmpId", empId);
            uri.AddQuery("TimeCardStartDate", timecardStartDate.ToString());
            uri.AddQuery("TimeCardEndDate", timecardEndDate.ToString());

            var service = new HttpHelper<ObservableCollection<ATGetTimeCardLicensesByDateResponse>>();

            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);

           
            return data;
        }

        public async Task<List<ATGetPayrollTimecardsPeriodResponse>> ATGetMobileTimecardPeriods(string compId,string empId)
        {
            var url = $"{apiUrl}{Module}/{nameof(ATGetMobileTimecardPeriods)}";
            var uri = new UriBuilder(url);
            uri.AddQuery("ParamCompID", compId);
            uri.AddQuery("ParamEmpID", empId);

            var service = new HttpHelper<List<ATGetPayrollTimecardsPeriodResponse>>();

            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);


            return data;
        }

        public async Task<ATGetPayrollTimecardsPeriodResponse> ATGetMobileLastTimecardPeriod(string compId,string employeeID)
        {
            var url = $"{apiUrl}{Module}/{nameof(ATGetMobileLastTimecardPeriod)}";
            var uri = new UriBuilder(url);
            uri.AddQuery("ParamCompID", compId);
            uri.AddQuery("ParamEmpID", employeeID);

            var service = new HttpHelper<ATGetPayrollTimecardsPeriodResponse>();

            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);


            return data;
        }

        public async Task<List<ScheduleResponse>> ATGetMobileEmployeeSchedules(string ParamCompID, string ParamEmpID, DateTime FromPeriod, DateTime ToPeriod)
        {
            var url = $"{apiUrl}{Module}/{nameof(ATGetMobileEmployeeSchedules)}";
            var uri = new UriBuilder(url);
            uri.AddQuery("ParamCompID", ParamCompID);
            uri.AddQuery("ParamEmpID", ParamEmpID);
            uri.AddQuery("FromPeriod", FromPeriod.ToString());
            uri.AddQuery("ToPeriod", ToPeriod.ToString());

            var service = new HttpHelper<List<ScheduleResponse>>();
         
            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);

            return data;
        }


        public async Task<SchedulePeriodResponse> ATGetMobileSchedulePeriodByDate(string compId,string empId, DateTime fromPeriod, DateTime toPeriod)
        {
            var url = $"{apiUrl}{Module}/{nameof(ATGetMobileSchedulePeriodByDate)}";
            var uri = new UriBuilder(url);
            uri.AddQuery("ParamCompID", compId);
            uri.AddQuery("ParamEmpID", empId);
            uri.AddQuery("FromPeriod", fromPeriod.ToString());
            uri.AddQuery("ToPeriod", toPeriod.ToString());
            

            var service = new HttpHelper<SchedulePeriodResponse>();

            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);


            return data;
        }

        public async Task<List<SchedulePeriodResponse>> ATGetMobileSchedulePeriodsList(string compId, string employeeID)
        {
            var url = $"{apiUrl}{Module}/{nameof(ATGetMobileSchedulePeriodsList)}";
            var uri = new UriBuilder(url);
            uri.AddQuery("ParamCompID", compId);
            uri.AddQuery("ParamEmpID", employeeID);

            var service = new HttpHelper<List<SchedulePeriodResponse>>();

            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);


            return data;
        }

        public async Task<SchedulePeriodResponse> ATGetMobileScheduleCurrentPeriod(string compId, string employeeID,DateTime periodDate)
        {
            var url = $"{apiUrl}{Module}/{nameof(ATGetMobileScheduleCurrentPeriod)}";
            var uri = new UriBuilder(url);
            uri.AddQuery("ParamCompID", compId);
            uri.AddQuery("ParamEmpID", employeeID);
            uri.AddQuery("ParamPeriodDate", periodDate.ToString());

            var service = new HttpHelper<SchedulePeriodResponse>();

            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);


            return data;
        }

        public async Task<GetRecordLicenseResponse> ATUpdateMobilePunches(int punchID , Nullable<int> departmentEntryNumID, Nullable<DateTime> punchTime , Nullable<bool> isPunchIn , int UpdateType)
        {
            var url = $"{apiUrl}{Module}/{nameof(ATUpdateMobilePunches)}";
            var uri = new UriBuilder(url);
            var model = new { PunchID = punchID, DepartmentId = departmentEntryNumID, PunchTime=punchTime, IsPunchIn= isPunchIn, updateType=UpdateType };
            var service = new HttpHelper<GetRecordLicenseResponse>();

            var data = await service.PostRestServiceDataAsync(url, model).ConfigureAwait(false);

            return data;
        }

        public async Task<ATGetLastPunchResponse> ATGetMobileLastPunch(string compId, string employeeID)
        {
            var url = $"{apiUrl}{Module}/{nameof(ATGetMobileLastPunch)}";
            var uri = new UriBuilder(url);
            uri.AddQuery("ParamCompID", compId);
            uri.AddQuery("ParamEmpID", employeeID);            

            var service = new HttpHelper<ATGetLastPunchResponse>();

            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);

            if (data != null) {
                if (data.isPunchIn) {
                    data.isPunchOut = false;
                }
                else
                {
                    data.isPunchOut = true;
                } 
            

            }

            return data;
        }


    }
}
