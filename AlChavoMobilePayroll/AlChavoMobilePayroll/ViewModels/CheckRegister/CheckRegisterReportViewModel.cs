using AlChavoMobilePayroll.Models.CheckRegister;
using Newtonsoft.Json;
using System;
using Xamarin.Forms;

namespace AlChavoMobilePayroll.ViewModels.CheckRegister
{
    [QueryProperty(nameof(content), nameof(content))]
    public class CheckRegisterReportViewModel : Common.BaseViewModel
    {

        private Uri PACheckRegisterViewReport { get; set; }
        public Uri paCheckRegisterViewReport { get => PACheckRegisterViewReport; set { PACheckRegisterViewReport = value; OnPropertyChanged(); } }

        public CheckRegisterReportViewModel()
        {
            Title = "Check Register Report" ;

            //GetCheckRegisterReport();

        }
        private GetCheckRegisterReportRequest GetCheckRegisterReport { get; set; }

        public GetCheckRegisterReportRequest getCheckRegisterReport
        {
            get => GetCheckRegisterReport;
            set
            {
                GetCheckRegisterReport = value;
                OnPropertyChanged();
            }
        }

        private string Content { get; set; }
        public string content
        {
            get => Content;
            set
            {
                Content = value;
                GetCheckRegisterReportByData(value);
                OnPropertyChanged();
            }
        }

        public async void GetCheckRegisterReportByData(string data)
        {
            isBusy = true;
            getCheckRegisterReport = JsonConvert.DeserializeObject<GetCheckRegisterReportRequest>(data);

            if (getCheckRegisterReport == null)
                return;

            paCheckRegisterViewReport = await CheckRegisterService.GetCheckRegisterReport(getCheckRegisterReport.Compid,
            getCheckRegisterReport.EmployeeId, getCheckRegisterReport.PayrollSequence, getCheckRegisterReport.CheckDate,
            getCheckRegisterReport.CheckNumber, getCheckRegisterReport.BankId, getCheckRegisterReport.NextCheckOrder);

            isBusy = false;

        }





    }
}
