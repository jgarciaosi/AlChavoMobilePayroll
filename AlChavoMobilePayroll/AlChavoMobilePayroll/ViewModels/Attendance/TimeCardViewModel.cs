using AlChavoMobilePayroll.Helpers;
using AlChavoMobilePayroll.Models.Attendance.Punches;
using AlChavoMobilePayroll.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Essentials;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;
using AlChavoMobilePayroll.Views.Attendance;
using Newtonsoft.Json;
using AlChavoMobilePayroll.Models.SManagement.Company;

namespace AlChavoMobilePayroll.ViewModels.Attendance
{
    public class TimeCardViewModel : Common.BaseViewModel
    {
        public ObservableCollection<Grouping<string, ATGetTimeCardByIdResponse>> timeCardEmployeeList { get => TimeCardEmployeeList; set { TimeCardEmployeeList = value; OnPropertyChanged(); } }
        private ObservableCollection<Grouping<string, ATGetTimeCardByIdResponse>> TimeCardEmployeeList { get; set; }

        public bool IsEnabledPunch { get => isEnabledPunch; set { isEnabledPunch = value; OnPropertyChanged(); } }
        private bool isEnabledPunch { get; set; }

        public List<ATGetTimeCardByIdResponse> timeCardEmployeeNonGroupedList { get => TimeCardEmployeeNonGroupedList; set { TimeCardEmployeeNonGroupedList = value; OnPropertyChanged(); } }
        private List<ATGetTimeCardByIdResponse> TimeCardEmployeeNonGroupedList { get; set; }
        public Xamarin.CommunityToolkit.ObjectModel.IAsyncCommand<object> PeriodsChangedCommand { get; set; }
        public Xamarin.CommunityToolkit.ObjectModel.IAsyncCommand<object> IPreviousPeriodCommand { get; set; }
        public Xamarin.CommunityToolkit.ObjectModel.IAsyncCommand<object> INextPeriodCommand { get; set; }
        public Xamarin.CommunityToolkit.ObjectModel.IAsyncCommand<object> GoToPunchInViewCommand { get; set; }
        public Xamarin.CommunityToolkit.ObjectModel.IAsyncCommand<object> GoToPunchOutViewCommand { get; set; }



        public ObservableCollection<ATGetTimeCardLicensesByDateResponse> timeCardEmployeeLicenses { get => TimeCardEmployeeLicenses; set { TimeCardEmployeeLicenses = value; OnPropertyChanged(); } }
        private ObservableCollection<ATGetTimeCardLicensesByDateResponse> TimeCardEmployeeLicenses { get; set; }
        public ATGetPayrollTimecardsPeriodResponse selectedTimecardPeriod { get => SelectedTimecardPeriod; set { SelectedTimecardPeriod = value; OnPropertyChanged(); } }
        private ATGetPayrollTimecardsPeriodResponse SelectedTimecardPeriod;

        public List<ATGetPayrollTimecardsPeriodResponse> timeCardPeriodsList { get => TimeCardPeriodsList; set { TimeCardPeriodsList = value; OnPropertyChanged(); } }
        private List<ATGetPayrollTimecardsPeriodResponse> TimeCardPeriodsList { get; set; }

        public List<ATGetPayrollTimecardsPeriodResponse> timeCardPeriodsListNonProp { get => TimeCardPeriodsListNonProp; set { TimeCardPeriodsListNonProp = value; } }
        private List<ATGetPayrollTimecardsPeriodResponse> TimeCardPeriodsListNonProp { get; set; }

        public async Task InitializeData()
        {
            if (!isBusy) 
            isBusy = true;

            await LoadAttendanceTimeCard();
            isBusy = false;
        }

        public bool isFirstTime;

        public TimeCardViewModel()
        {
            GoToPunchInViewCommand = new Xamarin.CommunityToolkit.ObjectModel.AsyncCommand<object>((item) => GoToEditIn(item));
            GoToPunchOutViewCommand = new Xamarin.CommunityToolkit.ObjectModel.AsyncCommand<object>((item) => GoToEditOut(item));
            Title = "Timecard";
            isFirstTime = true;
            PeriodsChangedCommand = new Xamarin.CommunityToolkit.ObjectModel.AsyncCommand<object>((item) => GetSelectedPeriod(item));
            IPreviousPeriodCommand = new Xamarin.CommunityToolkit.ObjectModel.AsyncCommand<object>((item) => GetPreviousPeriod());
            INextPeriodCommand = new Xamarin.CommunityToolkit.ObjectModel.AsyncCommand<object>((item) => GetNextPeriod());
            
        }

        public async Task<List<DepartmentByCompaniesResponse>> GetDepartmentByCompaines(string id)
        {
            if (string.IsNullOrEmpty(id))
                return default;

            var data = await PunchService.GetDepartmentByCompId(id).ConfigureAwait(false);
            return data;
        }

        public async Task GoToEditIn(object data)
        {
            isBusy = true;

            //Departamentos para que vayan cargados desde aquí
            var CompID = await SecureStorage.GetAsync("CompId");
            List<DepartmentByCompaniesResponse> departmentList = new List<DepartmentByCompaniesResponse>();
            departmentList = await GetDepartmentByCompaines(CompID);


            ATGetTimeCardByIdResponse timeCardPeriodsRedirect = (ATGetTimeCardByIdResponse)data;

            timeCardPeriodsRedirect.IsPunchInFlagForEdit = true;
            timeCardPeriodsRedirect.departmentList = departmentList;

            var model = JsonConvert.SerializeObject(timeCardPeriodsRedirect);

            await SecureStorage.SetAsync("selectedPeriodID", selectedTimecardPeriod.ID.ToString());

            isBusy = false;

            await Shell.Current.GoToAsync($"/{nameof(TimeCardEdit)}?{nameof(TimeCardEditViewModel.content)}={model}");

        }

        public async Task GoToEditOut(object data)
        {
            isBusy = true;
            //Departamentos para que vayan cargados desde aquí
            var CompID = await SecureStorage.GetAsync("CompId");
            List<DepartmentByCompaniesResponse> departmentList = new List<DepartmentByCompaniesResponse>();
            departmentList = await GetDepartmentByCompaines(CompID);


            ATGetTimeCardByIdResponse timeCardPeriodsRedirect = (ATGetTimeCardByIdResponse)data;

            timeCardPeriodsRedirect.IsPunchInFlagForEdit = false;
            timeCardPeriodsRedirect.departmentList = departmentList;

            var model = JsonConvert.SerializeObject(timeCardPeriodsRedirect);

            await SecureStorage.SetAsync("selectedPeriodID", selectedTimecardPeriod.ID.ToString());

            isBusy = false;

            await Shell.Current.GoToAsync($"/{nameof(TimeCardEdit)}?{nameof(TimeCardEditViewModel.content)}={model}");

        }



        private async Task GetSelectedPeriod(object item)
        {

            var userGUID = await SecureStorage.GetAsync("UserGUID");
            string CompID = await SecureStorage.GetAsync("CompId");
            string EmpId = await SecureStorage.GetAsync("EmpId");
            var SelectedItem = item as RadComboBox;

            //SelectedTimecardPeriod = SelectedItem.SelectedItem as ATGetPayrollTimecardsPeriodResponse;


            //var storedSessionPeriodID = await SecureStorage.GetAsync("selectedPeriodID");

            //if (storedSessionPeriodID != null)
            //{
            //    ATGetPayrollTimecardsPeriodResponse TimecardSessioned = timeCardPeriodsListNonProp.Find(x => x.ID == Int32.Parse(storedSessionPeriodID));
            //    selectedTimecardPeriod = TimecardSessioned;

            //    timeCardEmployeeNonGroupedList = await ATGetMobileTimeCardByIDNonGrouped(CompID, EmpId, selectedTimecardPeriod.ID, userGUID).ConfigureAwait(false);
            //    timeCardEmployeeLicenses = await ATGetMobileTimecardLicenses(CompID, EmpId, selectedTimecardPeriod.PayrollStartPeriod, selectedTimecardPeriod.PayrollEndPeriod);

            //    SelectedIndex = timeCardPeriodsListNonProp.FindIndex(x => x.ID == Int32.Parse(storedSessionPeriodID));
            //    SecureStorage.Remove("selectedPeriodID");
            //}
            //else { 
            selectedTimecardPeriod = SelectedItem.SelectedItem as ATGetPayrollTimecardsPeriodResponse;

            if (!isBusy)
                isBusy = true;

            timeCardEmployeeNonGroupedList = await ATGetMobileTimeCardByIDNonGrouped(CompID, EmpId, selectedTimecardPeriod.ID, userGUID).ConfigureAwait(false);
            timeCardEmployeeLicenses = await ATGetMobileTimecardLicenses(CompID, EmpId, selectedTimecardPeriod.PayrollStartPeriod, selectedTimecardPeriod.PayrollEndPeriod);

            isBusy = false;

            //SelectedIndex = timeCardPeriodsListNonProp.FindIndex(x => x.ID == selectedTimecardPeriod.ID);
            //}
            //}
            //else
            //{
            //    try
            //    {

            //        ATGetPayrollTimecardsPeriodResponse TimecardSessioned = timeCardPeriodsListNonProp.Find(x => x.ID == Int32.Parse(storedSessionPeriodID));

            //        selectedTimecardPeriod = TimecardSessioned;

            //        timeCardEmployeeNonGroupedList = await ATGetMobileTimeCardByIDNonGrouped(CompID, EmpId, TimecardSessioned.ID, userGUID).ConfigureAwait(false);
            //        timeCardEmployeeLicenses = await ATGetMobileTimecardLicenses(CompID, EmpId, TimecardSessioned.PayrollStartPeriod, TimecardSessioned.PayrollEndPeriod);

            //        SecureStorage.Remove("selectedPeriodID");
            //    }
            //    catch (Exception ex)
            //    {
            //        var mesage = ex.Message;
            //        throw;
            //    }

            //}




            










        }

        private async Task GetPreviousPeriod()
        {
            isBusy = true;

            var index = timeCardPeriodsList.IndexOf(selectedTimecardPeriod);

            if (index < timeCardPeriodsList.Count - 1)
                selectedTimecardPeriod = timeCardPeriodsList[index + 1];

            isBusy = false;

            //isBusy = true;
            //if (this.selectedIndex < timeCardPeriodsListNonProp.Count - 1)
            //{
            //    this.SelectedIndex = this.selectedIndex + 1;
            //}
            //isBusy = false;
        }

        private async Task GetNextPeriod()
        {

            isBusy = true;

            var index = timeCardPeriodsList.IndexOf(selectedTimecardPeriod);

            if (index != 0)
                selectedTimecardPeriod = timeCardPeriodsList[index - 1];

            isBusy = false;

            //isBusy = true;
            //if (this.selectedIndex != 0)
            //{
            //    this.SelectedIndex = this.selectedIndex - 1;
            //}
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

        private async Task LoadAttendanceTimeCard()
        { //Seguridad de edit punches
            var IsEnablePunchSession = await SecureStorage.GetAsync("PAMobileEditPunchYN");
            if (IsEnablePunchSession != null)
            {
                IsEnabledPunch = (Int32.Parse(IsEnablePunchSession) == 1) ? true : false;
            }

            string CompID = await SecureStorage.GetAsync("CompId");
            string EmpId = await SecureStorage.GetAsync("EmpId");
            var userGUID = await SecureStorage.GetAsync("UserGUID");

            timeCardPeriodsListNonProp = await ATGetMobileTimecardPeriods(CompID, EmpId);
            timeCardPeriodsList = timeCardPeriodsListNonProp;

            var SelectedItem = await ATGetMobileLastTimecardPeriod(CompID, EmpId);
            selectedTimecardPeriod = timeCardPeriodsList.Find(x => x.ID.Equals(SelectedItem.ID));

            // SelectedIndex = timeCardPeriodsListNonProp.FindIndex(x => x.ID == selectedTimecardPeriod.ID);
            
           
        }

        public async Task<ObservableCollection<Grouping<string, ATGetTimeCardByIdResponse>>> ATGetMobileTimeCardByID(string compId, string empId, int periodId, string userId)
        {
            var data = await AttendanceService.ATGetMobileTimeCardByPeriodID(compId, empId, periodId, userId).ConfigureAwait(false);


            var sorted = from f in data
                         orderby f.PunchDay
                         group f by f.PunchDay.ToString()
                         into TheGroup
                         select new Grouping<string, ATGetTimeCardByIdResponse>(TheGroup.Key, TheGroup);



            return new ObservableCollection<Grouping<string, ATGetTimeCardByIdResponse>>(sorted);



        }

        public async Task<List<ATGetTimeCardByIdResponse>> ATGetMobileTimeCardByIDNonGrouped(string compId, string empId, int periodId, string userId)
        {
            var data = await AttendanceService.ATGetMobileTimeCardByPeriodID(compId, empId, periodId, userId).ConfigureAwait(false);

            return data;

        }

        public async Task<ObservableCollection<ATGetTimeCardLicensesByDateResponse>> ATGetMobileTimecardLicenses(string compId, string empId, DateTime timecardStartDate, DateTime timecardEndDate)
        {
            var data = await AttendanceService.ATGetMobileTimecardLicenses(compId, empId, timecardStartDate, timecardEndDate).ConfigureAwait(false);
            return data;

        }

        public async Task<List<ATGetPayrollTimecardsPeriodResponse>> ATGetMobileTimecardPeriods(string compId, string empId)
        {
            var data = await AttendanceService.ATGetMobileTimecardPeriods(compId, empId).ConfigureAwait(false);
            return data;

        }

        public async Task<ATGetPayrollTimecardsPeriodResponse> ATGetMobileLastTimecardPeriod(string compId, string empId)
        {
            var data = await AttendanceService.ATGetMobileLastTimecardPeriod(compId, empId).ConfigureAwait(false);
            return data;

        }
    }



}
