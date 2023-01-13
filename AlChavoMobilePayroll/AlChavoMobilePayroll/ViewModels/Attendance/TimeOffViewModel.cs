using AlChavoMobilePayroll.Models.Attendance;
using AlChavoMobilePayroll.Models.Attendance.Punches;
using AlChavoMobilePayroll.Models.Bill;
using AlChavoMobilePayroll.Services;
using FluentValidation;
using FluentValidation.Results;
using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Documents.Common.Model;
using Telerik.XamarinForms.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.TizenSpecific;

namespace AlChavoMobilePayroll.ViewModels.Attendance
{
    public class TimeOffViewModel : Common.BaseViewModel
    {

        private string errorMessage;
        public String ErrorMessage { get => errorMessage; set { errorMessage = value; OnPropertyChanged(); } }


        private LicenseRequest licenseRequest = new LicenseRequest { };
        public LicenseRequest LicenseRequest { get => licenseRequest; set { licenseRequest = value; OnPropertyChanged(); } }

        public List<GetDefaultEarningsLicensesResponse> licenseDropDownList { get; set; }
        public List<GetDefaultEarningsLicensesResponse> LicenseDropDownList { get => licenseDropDownList; set { licenseDropDownList = value; OnPropertyChanged(); } }

        public GetDefaultEarningsLicensesResponse selectedLicenseRequest { get; set; }
        public GetDefaultEarningsLicensesResponse SelectedLicenseRequest { get => selectedLicenseRequest; set { selectedLicenseRequest = value; OnPropertyChanged(); } }


        private int selectedTimeIndex = -1;
        public int SelectedTimeIndex { get { return this.selectedTimeIndex; } set { if (this.selectedTimeIndex != value) { this.selectedTimeIndex = value; this.OnPropertyChanged(); } } }







        public Xamarin.CommunityToolkit.ObjectModel.IAsyncCommand<object> SelectedPendingOrProcessCommand { get; set; }
        public Xamarin.CommunityToolkit.ObjectModel.IAsyncCommand<object> NativeLoadedPendingOrProcessCommand { get; set; }
        public DateTime licenseFromDate { get => LicenseFromDate; set { LicenseFromDate = value; OnPropertyChanged(); } }
        private DateTime LicenseFromDate { get; set; }

        public DateTime licenseToDate { get => LicenseToDate; set { LicenseToDate = value; OnPropertyChanged(); } }
        private DateTime LicenseToDate { get; set; }
        public int selectedIndexLicenseSegmented { get => SelectedIndexLicenseSegmented; set { SelectedIndexLicenseSegmented = value; OnPropertyChanged(); } }
        private int SelectedIndexLicenseSegmented { get; set; }

        public IAsyncCommand RequestTimeOffCommand { get; set; }
        public IAsyncCommand<object> DeleteRequestTimeOffCommand { get; set; }
        public IAsyncCommand<object> LicensesChangedCommand { get; set; }

        public ObservableCollection<string> PendingLicenses { get; private set; }
        public ObservableCollection<string> ProcessedLicenses { get; private set; }




        private GetDefaultEarningsLicensesRequest GetLicensesDropDownListRequest = new GetDefaultEarningsLicensesRequest { };

        public GetDefaultEarningsLicensesRequest getLicensesDropDownListRequest
        {
            get => GetLicensesDropDownListRequest;
            set
            {
                GetLicensesDropDownListRequest = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<GetLicenseByStatusResponse> licensePendingRequestList { get => LicensePendingRequestList; set { LicensePendingRequestList = value; OnPropertyChanged(); } }

        private ObservableCollection<GetLicenseByStatusResponse> LicensePendingRequestList { get; set; }

        public ObservableCollection<GetLicenseByStatusResponse> licenseProcessedRequestList { get => LicenseProcessedRequestList; set { LicenseProcessedRequestList = value; OnPropertyChanged(); } }

        private ObservableCollection<GetLicenseByStatusResponse> LicenseProcessedRequestList { get; set; }
        public int ID { get; set; }
        public float hoursLicense { get; set; }
        public string PendingTitle { get; set; }
        public string selectedLicense { get => SelectedLicense; set { SelectedLicense = value; OnPropertyChanged(); } }
        public string SelectedLicense { get; set; }
        public bool isVisibleProcess { get => IsVisibleProcess; set { IsVisibleProcess = value; OnPropertyChanged(); } }
        public bool IsVisibleProcess { get; set; }
        public FontAttributes pendingFontAttributes { get => PendingFontAttributes; set { PendingFontAttributes = value; OnPropertyChanged(); } }
        public FontAttributes PendingFontAttributes { get; set; }
        public FontAttributes processedFontAttributes { get => ProcessedFontAttributes; set { ProcessedFontAttributes = value; OnPropertyChanged(); } }
        public FontAttributes ProcessedFontAttributes { get; set; }
        public System.Drawing.Color pendingColorAttributes { get => PendingColorAttributes; set { PendingColorAttributes = value; OnPropertyChanged(); } }
        public System.Drawing.Color PendingColorAttributes { get; set; }
        public System.Drawing.Color processedColorAttributes { get => ProcessedColorAttributes; set { ProcessedColorAttributes = value; OnPropertyChanged(); } }
        public System.Drawing.Color ProcessedColorAttributes { get; set; }
        public bool isVisiblePending { get => IsVisiblePending; set { IsVisiblePending = value; OnPropertyChanged(); } }
        public bool IsVisiblePending { get; set; }
        public string pendingTitle { get => PendingTitle; set { PendingTitle = value; OnPropertyChanged(); } }
        public string ProcessedTitle { get; set; }
        public string processedTitle { get => ProcessedTitle; set { ProcessedTitle = value; OnPropertyChanged(); } }
        private ObservableCollection<string> headerTitles { get; set; }
        public ObservableCollection<string> HeaderTitles { get => headerTitles; set { headerTitles = value; OnPropertyChanged(); } }





        /////////////////////////
        private string selectedLicenseTimeOff;
        public string SelectedLicenseTimeOff
        {
            get
            {
                return this.selectedLicenseTimeOff;
            }
            set
            {
                if (this.selectedLicenseTimeOff != value)
                {
                    this.selectedLicenseTimeOff = value;
                    this.OnPropertyChanged();
                }
            }
        }


        private ICommand tapPendingCommand;
        public ICommand TapPendingCommand => (tapPendingCommand = new Command(OnPendingTapped));

        void OnPendingTapped()
        {

            isVisiblePending = true;
            isVisibleProcess = false;
            pendingFontAttributes = FontAttributes.Bold;
            processedFontAttributes = FontAttributes.None;
            pendingColorAttributes = System.Drawing.Color.FromArgb(22, 115, 195);
            processedColorAttributes = System.Drawing.Color.FromArgb(238, 238, 238);
        }

        private ICommand tapProcessedCommand;
        public ICommand TapProcessedCommand => (tapProcessedCommand = new Command(OnProcessedTapped));

        void OnProcessedTapped()
        {
            isVisiblePending = false;
            isVisibleProcess = true;
            pendingFontAttributes = FontAttributes.None;
            processedFontAttributes = FontAttributes.Bold;
            processedColorAttributes = System.Drawing.Color.FromArgb(22, 115, 195);
            pendingColorAttributes = System.Drawing.Color.FromArgb(238, 238, 238);

        }




        public TimeOffViewModel()
        {
            isVisiblePending = true;
            isVisibleProcess = false;
            Title = "Time Off";

            RequestTimeOffCommand = new AsyncCommand(RequestTimeOff);
            DeleteRequestTimeOffCommand = new AsyncCommand<object>(DeleteRequestTimeOff);
            LicensesChangedCommand = new AsyncCommand<object>((item) => GetSelectedLicense(item));

            LicenseRequest.RequestDateFrom = DateTime.Now;
            LicenseRequest.RequestDateTo = DateTime.Now;


        }


        private async Task GetSelectedLicense(object item)
        {
            var SelectedItem = item as Picker;
            SelectedLicenseRequest = SelectedItem.SelectedItem as GetDefaultEarningsLicensesResponse;
            LicenseRequest.RequestLicenseEarning = SelectedLicenseRequest.EID;
        }


        public async Task DeleteRequestTimeOff(object item)
        {
            isBusy = true;

            //Primero se elimina la licencia
            GetLicenseByStatusResponse deleteLicenseObject = (GetLicenseByStatusResponse)item;

            await DeleteRecordLicense(deleteLicenseObject.CompID, deleteLicenseObject.ID).ConfigureAwait(false);

            //Luego se vuelven a cargar la licencias para refrescar
            string CompID = await SecureStorage.GetAsync("CompId");
            string EmpId = await SecureStorage.GetAsync("EmpId");

            licensePendingRequestList = await GetTimeOffDataPending(new GetLicenseByStatusRequest() { CompID = CompID, EmployeeID = EmpId, Processed = false }).ConfigureAwait(false);

            pendingTitle = "Pending (" + licensePendingRequestList.Count.ToString() + ")";

            OnPendingTapped();
            isBusy = false;

        }

        public async Task RequestTimeOff()
        {
            isBusy = true;

            string CompID = await SecureStorage.GetAsync("CompId");
            string EmpId = await SecureStorage.GetAsync("EmpId");
           
            ErrorMessage = String.Empty;
            ValidationResult result = new LicenseRequestValidator().Validate(LicenseRequest);

            if (result.IsValid)
            {
                await GetRecordLicense(LicenseRequest).ConfigureAwait(false);

                licensePendingRequestList = await GetTimeOffDataPending(new GetLicenseByStatusRequest() { CompID = CompID, EmployeeID = EmpId, Processed = false }).ConfigureAwait(false);
                pendingTitle = "Pending (" + licensePendingRequestList.Count.ToString() + ")";

                isBusy = false;
               
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await ShowNotificationPopup(String.Empty, "License Registered Succesfully", String.Empty, NotificationType.Success, String.Empty, false);
                });

                OnPendingTapped();
            }
            else
            {
                foreach (var error in result.Errors) { ErrorMessage = error.ErrorMessage; }

                isBusy = false;

                ShowACToastMessage(ErrorMessage, true);
            }




        }



        public async Task GetRecordLicense(LicenseRequest getRecordLicenseRequest)
        {
            await AttendanceService.ATRecordLicense(getRecordLicenseRequest).ConfigureAwait(false);

        }

        public async Task DeleteRecordLicense(String CompID, int IdLicense)
        {
            await AttendanceService.ATMobileDeleteLicense(CompID, IdLicense).ConfigureAwait(false);

        }

        public void SendEmail(string Subject, string Body, string Recipient)
        {

            MailService.SendEmail(Subject, Body, Recipient);

        }
        public async Task<ObservableCollection<GetLicenseByStatusResponse>> GetTimeOffData(GetLicenseByStatusRequest getLicenseByStatusRequest)
        {
            var data = await AttendanceService.ATGetSSPLicensesByStatus(getLicenseByStatusRequest).ConfigureAwait(false);

            return data;
        }

        public async Task<ObservableCollection<GetLicenseByStatusResponse>> GetTimeOffDataPending(GetLicenseByStatusRequest getLicenseByStatusRequest)
        {
            var data = await AttendanceService.ATGetSSPLicensesByStatusPending(getLicenseByStatusRequest).ConfigureAwait(false);

            return data;
        }

        public async Task<List<GetDefaultEarningsLicensesResponse>> GetDefaultEarningsLicenses(GetDefaultEarningsLicensesRequest getDefaultEarningsLicensesRequest)
        {
            var data = await AttendanceService.ATGetDefaultEarningsLicenses(getDefaultEarningsLicensesRequest).ConfigureAwait(false);

            return data;
        }

        public async Task InitializeData()
        {

            isBusy = true;
            await LoadTimeOff();
            isBusy = false;
        }

        private async Task LoadTimeOff()
        {

            string CompID = await SecureStorage.GetAsync("CompId");
            string EmpId = await SecureStorage.GetAsync("EmpId");
            string EmpEntryNum = await SecureStorage.GetAsync("EmployeeEntryNum");
            string DeparmentId = await SecureStorage.GetAsync("DepartmentId");


            LicenseRequest.CompId = CompID;
            LicenseRequest.EmpId = EmpId;
            LicenseRequest.EmployeeEntryNum = Int32.Parse(EmpEntryNum);
            LicenseRequest.DepartmentID = DeparmentId;
            LicenseRequest.RequestDateRequested = DateTime.Today;
            LicenseRequest.RequestComments = "Requested in Mobile App";

            //Pending
            licensePendingRequestList = await GetTimeOffDataPending(new GetLicenseByStatusRequest() { CompID = CompID, EmployeeID = EmpId, Processed = false }).ConfigureAwait(false);

            pendingTitle = "Pending (" + licensePendingRequestList.Count.ToString() + ")";

            //Processed
            licenseProcessedRequestList = await GetTimeOffData(new GetLicenseByStatusRequest() { CompID = CompID, EmployeeID = EmpId, Processed = true }).ConfigureAwait(false);

            processedTitle = "Processed (" + licenseProcessedRequestList.Count.ToString() + ")";

            getLicensesDropDownListRequest.Compid = CompID;
            getLicensesDropDownListRequest.Lang = "ENG";
            LicenseDropDownList = await GetDefaultEarningsLicenses(getLicensesDropDownListRequest);

            if (licensePendingRequestList.Count == 0 && licenseProcessedRequestList.Count >= 0)
            {
                OnProcessedTapped();
            }
            else
            {
                OnPendingTapped();
            }



        }





    }
}
