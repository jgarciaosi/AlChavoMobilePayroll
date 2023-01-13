
using AlChavoMobilePayroll.Views.DownloadDocuments;
using AlChavoMobilePayroll.Views.Attendance;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace AlChavoMobilePayroll.ViewModels.SManagement
{
    public class MoreIndexViewModel : Common.BaseViewModel
    {


        public IAsyncCommand<object> GoToSickAndVacHistoryReportCommand { get; set; }
        public IAsyncCommand<object> GoToScheduleCommand { get; set; }
        public IAsyncCommand<object> GoToDownloadDocumentsCommand { get; set; }



        public MoreIndexViewModel()
        {
            Title = "More";

            GoToSickAndVacHistoryReportCommand = new AsyncCommand<object>((item) => GoToSickAndVacHistoryReport(item));
            GoToScheduleCommand = new AsyncCommand<object>((item) => GoToSchedule(item));
            GoToDownloadDocumentsCommand = new AsyncCommand<object>((item) => GoToDownloadDocuments(item));

        }
        private async Task GoToSickAndVacHistoryReport(object data)
        {
            await Shell.Current.GoToAsync($"/{nameof(TimeOffReport)}").ConfigureAwait(false);
        }
        private async Task GoToSchedule(object data)
        {
           await Shell.Current.GoToAsync($"/{nameof(Schedule)}").ConfigureAwait(false);
        }
        private async Task GoToDownloadDocuments(object data)
        {
            await Shell.Current.GoToAsync($"/{nameof(DocumentsIndex)}").ConfigureAwait(false);
        }

    }
}
