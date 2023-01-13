using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using AlChavoMobilePayroll.Views.Attendance;

namespace AlChavoMobilePayroll.ViewModels.Attendance
{
    public class TimeOffViewReportModel : Common.BaseViewModel
    {
        public IAsyncCommand<object> GoToTimeOffViewReportCommand { get; set; }
        public TimeOffViewReportModel()
        {
            Title = "Sick and Vacation History";
            GoToTimeOffViewReportCommand = new AsyncCommand<object>((item) => GoTimeOffViewReportReport());
             
            dateFrom = DateTime.Now.AddDays(-90);
            dateTo = DateTime.Now;
        }

        public async Task GoTimeOffViewReportReport()
        {

            //var model = JsonConvert.SerializeObject(checkRegisterDetailData);
            await Shell.Current.GoToAsync($"/{nameof(TimeOffReportDetail)}?{nameof(TimeOffDetailViewModel.dateFrom)}={dateFrom}&{nameof(TimeOffDetailViewModel.dateTo)}={dateTo}");

          
        }

        private DateTime DateFrom { get; set; }
        public DateTime dateFrom
        {
            get => DateFrom;
            set
            {
                DateFrom = value;

                OnPropertyChanged();
            }
        }

        private DateTime DateTo { get; set; }
        public DateTime dateTo
        {
            get => DateTo;
            set
            {
                DateTo = value;
                OnPropertyChanged();
            }
        }


        public async void InitializeData()
        {

            isBusy = true;
            await LoadTimeOff();
            isBusy = false;
        }

        private async Task LoadTimeOff()
        {
           
        }




    }
}
