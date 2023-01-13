using AlChavoMobilePayroll.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AlChavoMobilePayroll.ViewModels.Attendance
{
    [QueryProperty(nameof(dateFrom), nameof(dateFrom))]
    [QueryProperty(nameof(dateTo), nameof(dateTo))]
    public class TimeOffDetailViewModel: Common.BaseViewModel

    {
     
        private string DateFrom { get; set; }
        public string dateFrom
        {
            get => DateFrom;
            set
            {
                DateFrom = value;
                
                OnPropertyChanged();
            }
        }

        private string DateTo { get; set; }
        public string dateTo
        {
            get => DateTo;
            set
            {
                DateTo = value;
                GetTimeOffReport();
                OnPropertyChanged();
            }
        }

        private Uri PATimeOffViewReport { get; set; }
        public Uri paTimeOffViewReport { get => PATimeOffViewReport; set { PATimeOffViewReport = value; OnPropertyChanged(); } }

        
        public TimeOffDetailViewModel()
        {
            Title = "Sick and Vacation History";



        }

        public async void GetTimeOffReport()
        {
           
           paTimeOffViewReport = await AttendanceService.ATTimeOffMobileReport(await SecureStorage.GetAsync("CompId"),
               await SecureStorage.GetAsync("UserGUID"), await SecureStorage.GetAsync("EmpId"),
               Convert.ToDateTime(dateFrom),Convert.ToDateTime(dateTo));

        }
    }
}
