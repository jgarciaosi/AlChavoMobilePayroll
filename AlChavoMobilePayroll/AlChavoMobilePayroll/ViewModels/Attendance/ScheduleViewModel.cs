using AlChavoMobilePayroll.Models.Attendance;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


using AlChavoMobilePayroll.Services;
using Xamarin.Essentials;
using Telerik.XamarinForms.Input;
using AlChavoMobilePayroll.Models.Attendance.Punches;

namespace AlChavoMobilePayroll.ViewModels.Attendance
{
    public class ScheduleViewModel : Common.BaseViewModel
    {
        private List<SchedulePeriodResponse> schedulePeriodsListNonProp { get; set; }
        public List<SchedulePeriodResponse> schedulePeriodsList { get => SchedulePeriodsList; set { SchedulePeriodsList = value; OnPropertyChanged(); } }
        private List<SchedulePeriodResponse> SchedulePeriodsList { get; set; }
        public List<ScheduleResponse> scheduleList { get => ScheduleList; set { ScheduleList = value; OnPropertyChanged(); } }
        private List<ScheduleResponse> ScheduleList { get; set; }
        public Xamarin.CommunityToolkit.ObjectModel.IAsyncCommand<object> PeriodsChangedCommand { get; set; }
        public Xamarin.CommunityToolkit.ObjectModel.IAsyncCommand<object> IPreviousPeriodCommand { get; set; }
        public Xamarin.CommunityToolkit.ObjectModel.IAsyncCommand<object> INextPeriodCommand { get; set; }
        public async void InitializeData()
        {
            //isBusy = true;
            await LoadAttendanceSchedules();
            //isBusy = false;
        }

        public SchedulePeriodResponse selectedSchedulePeriod { get => SelectedSchedulePeriod; set { SelectedSchedulePeriod = value; OnPropertyChanged(); } }
        private SchedulePeriodResponse SelectedSchedulePeriod;

        public ScheduleViewModel()
        {
            Title = "Schedule";
            PeriodsChangedCommand = new Xamarin.CommunityToolkit.ObjectModel.AsyncCommand<object>((item) => GetSelectedPeriod(item));
            IPreviousPeriodCommand = new Xamarin.CommunityToolkit.ObjectModel.AsyncCommand<object>((item) => GetPreviousPeriod());
            INextPeriodCommand = new Xamarin.CommunityToolkit.ObjectModel.AsyncCommand<object>((item) => GetNextPeriod());
        }

        private async Task LoadAttendanceSchedules()
        {
            string compId = await SecureStorage.GetAsync("CompId");
            string empId = await SecureStorage.GetAsync("EmpId");



            selectedSchedulePeriod = await ATGetMobileScheduleCurrentPeriod(compId, empId, DateTime.Now);

            schedulePeriodsListNonProp = await ATGetMobileSchedulePeriodsList(compId, empId);

            if (selectedSchedulePeriod != null) { 
            
                SelectedIndex = schedulePeriodsListNonProp.FindIndex(x => x.PeriodDisplay == selectedSchedulePeriod.PeriodDisplay);
            }

                schedulePeriodsList = schedulePeriodsListNonProp;




        }

        public async Task<List<ScheduleResponse>> ATGetMobileEmployeeSchedules(string compId, string empID, DateTime fromDate, DateTime toDate)
        {
            var data = await AttendanceService.ATGetMobileEmployeeSchedules(compId, empID, fromDate, toDate).ConfigureAwait(false);
            return data;

        }


        public async Task<SchedulePeriodResponse> ATGetMobileSchedulePeriodByDate(string compId, string empId, DateTime fromPeriod, DateTime toPeriod)
        {
            var data = await AttendanceService.ATGetMobileSchedulePeriodByDate(compId, empId, fromPeriod, toPeriod).ConfigureAwait(false);
            return data;

        }

        public async Task<List<SchedulePeriodResponse>> ATGetMobileSchedulePeriodsList(string compId, string employeeID)
        {
            var data = await AttendanceService.ATGetMobileSchedulePeriodsList(compId, employeeID).ConfigureAwait(false);
            return data;

        }

        public async Task<SchedulePeriodResponse> ATGetMobileScheduleCurrentPeriod(string compId, string empId, DateTime PeriodDate)
        {
            var data = await AttendanceService.ATGetMobileScheduleCurrentPeriod(compId, empId, PeriodDate).ConfigureAwait(false);
            return data;

        }

        private async Task GetSelectedPeriod(object item)
        {

            var userGUID = await SecureStorage.GetAsync("UserGUID");
            string CompID = await SecureStorage.GetAsync("CompId");
            string EmpId = await SecureStorage.GetAsync("EmpId");
            var SelectedItem = item as RadComboBox;

            SelectedSchedulePeriod = SelectedItem.SelectedItem as SchedulePeriodResponse;

            isBusy = true;
            if (SelectedSchedulePeriod != null)
            {
                scheduleList = await ATGetMobileEmployeeSchedules(CompID, EmpId, SelectedSchedulePeriod.FromPeriod, SelectedSchedulePeriod.ToPeriod).ConfigureAwait(false);
            }

            isBusy = false;



        }

        private async Task GetPreviousPeriod()
        {
            //isBusy = true;
            if (this.selectedIndex < schedulePeriodsListNonProp.Count - 1) { 
            this.SelectedIndex = this.selectedIndex + 1;
            }
            //isBusy = false;
        }

        private async Task GetNextPeriod()
        {
            //isBusy = true;
            if (this.selectedIndex != 0)
            {
                this.SelectedIndex = this.selectedIndex - 1;
            }
            //isBusy = false;
        }

        private int selectedIndex;
        public int SelectedIndex
        {
            get
            {
                return this.selectedIndex;
            }
            set
            {
                if (this.selectedIndex != value)
                {
                    this.selectedIndex = value;
                    OnPropertyChanged();
                }
            }
        }


    }
}
