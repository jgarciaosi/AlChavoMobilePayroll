using AlChavoMobilePayroll.Models;
using AlChavoMobilePayroll.Models.Attendance.Punches;
using AlChavoMobilePayroll.Models.SManagement.Company;
using AlChavoMobilePayroll.Models.SManagement.User;
using AlChavoMobilePayroll.Services;
using AlChavoMobilePayroll.Views.SManagement;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions; 
using System;
using System.Collections.Generic;
using System.Diagnostics; 
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks; 
using Telerik.XamarinForms.Input; 
using Xamarin.CommunityToolkit.ObjectModel; 
using Xamarin.Essentials;
using Xamarin.Forms; 

namespace AlChavoMobilePayroll.ViewModels.SManagement
{

    public class PunchViewModel : Common.BaseViewModel
    {
        private List<DepartmentByCompaniesResponse> departmentList { get; set; }
        private DepartmentByCompaniesResponse selectedDepartment { get; set; }

        private List<RecentPunchesResponse> lRecentPunchList;

        private string dateRecentPunch;


        public List<DepartmentByCompaniesResponse> DepartmentList { get => departmentList; set { departmentList = value; OnPropertyChanged(); } }
        public DepartmentByCompaniesResponse SelectedDepartment { get => selectedDepartment; set { selectedDepartment = value; OnPropertyChanged(); } }
        public string DateRecentPunch { get => dateRecentPunch; set { dateRecentPunch = value; OnPropertyChanged(); } }
        public List<RecentPunchesResponse> LRecentPunchList { get => lRecentPunchList; set { lRecentPunchList = value; OnPropertyChanged(); } }
        public DateTime CurrentDate { get; set; }


        public Command SelectedDepartmentCommand { get; set; }
        public IAsyncCommand<object> DoPunchCommand { get; set; } 


        /// ======================================================================   
        private SearchMode searchMode;
        private UserInfoResponse UserModel { get; set; }

        public DateTime cardDate;


        public PunchData punchData;
        public UserInfoResponse userModel { get => UserModel; set { UserModel = value; OnPropertyChanged(); } }
        public string AlertLocation { get; set; }
        public string alertLocation { get => AlertLocation; set { AlertLocation = value; OnPropertyChanged(); } }
        public string IconPinMap { get; set; }
        public string iconPinMap { get => IconPinMap; set { IconPinMap = value; OnPropertyChanged(); } }
        public bool PunchEnabled { get; set; }
        public bool punchEnabled { get => PunchEnabled; set { PunchEnabled = value; OnPropertyChanged(); } }
        private byte[] _canPhoto;
        private string _punchInOrOut;

        public IAsyncCommand GoToPunchOut { get; set; }
        public Command RefreshCommand { get; } 
        public byte[] CanPhoto { get => _canPhoto; set => SetProperty(ref _canPhoto, value); }
        public string PunchInOrOut { get => _punchInOrOut; set => SetProperty(ref _punchInOrOut, value); }
        public IAsyncCommand ShutterCommand { get; set; }


        public PunchViewModel()
        {
            Title = "Clock";
            SelectedDepartmentCommand = new Command((item) => SelectedDepartmentOnChange(item));
            RefreshCommand = new Command(async () => await GoToRefreshCommand()); 
            DoPunchCommand = new AsyncCommand<object>((item) =>  DoPunch(item), allowsMultipleExecutions: false); 
        }
  
        public async Task InitializeData()
        {
            isBusy = true;
            CurrentDate = DateTime.Now; 
            await LoadDepartmentInformation();
            await GetRecentPunches();
            await CheckGeoLocation(); 
            isBusy = false;
        }
        async Task GoToRefreshCommand() {
            
            try
            {
                await CheckGeoLocation();
                await GetRecentPunches();
                IsRefreshing = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            } 
        }
        private async Task LoadDepartmentInformation()
        {
            try
            {
                var CompID = await SecureStorage.GetAsync("CompId");
                var departmentList = await PunchService.GetDepartmentByCompId(CompID).ConfigureAwait(false);

                if (departmentList != null)
                {

                    string departmentId = await SecureStorage.GetAsync("DepartmentId");
                    DepartmentList = departmentList;
                    if (!string.IsNullOrEmpty(departmentId))
                    {
                        for (var i = 0; i < departmentList.Count; i++)
                        {
                            if (departmentList[i].DepartmentId == departmentId)
                                SelectedDepartment = departmentList[i];
                        }
                    }
                    else {
                        SelectedDepartment = departmentList[0];
                    }
                } 
            }
            catch (Exception e)
            {  
                ShowACToastMessage(e.InnerException.Message.ToString(), true);
            } 
        }  
        public async Task GetRecentPunches()
        {
            var recentPunchesResponses = new List<RecentPunchesResponse>();
            var CompID = await SecureStorage.GetAsync("CompId");
            string EmpId = await SecureStorage.GetAsync("EmpId");

            var data = await PunchService.ATGetSSPRecentPunches(CompID, EmpId).ConfigureAwait(false);

            foreach (var item in data.OrderByDescending(s => s.PunchTime))
            {
                var rPunches = new RecentPunchesResponse
                {
                    PunchID = item.PunchID,
                    PunchTime = item.PunchTime,
                    DepartmentId = item.DepartmentId,
                    PunchDesc = (item.isPunchIn) ? "PUNCH IN" : "PUNCH OUT",
                };

                recentPunchesResponses.Add(rPunches);
            }

            LRecentPunchList = recentPunchesResponses;
        } 
        private void SelectedDepartmentOnChange(object item)
        { 
            var selectedItem = item as RadComboBox;
            if (selectedItem != null)
            {
                SelectedDepartment = selectedItem.SelectedItem as DepartmentByCompaniesResponse;
            } 
        } 
        private async Task DoPunch(object objectType)
        {
            isBusy = true;
            string CompID = await SecureStorage.GetAsync("CompId");
            string EmpId = await SecureStorage.GetAsync("EmpId");
            string userGUID = await SecureStorage.GetAsync("UserGUID");
            string EmpEntryNum = await SecureStorage.GetAsync("EmployeeEntryNum");
            try
            { 
                var getIpAddress = GetDeviceIPAddress(); 
                Location currentLocation = await GetLocation();
                var currentLat = currentLocation.Latitude;
                var currentLon = currentLocation.Longitude;
                bool PunchButtonType = (PunchInOrOut == "PunchIn") ? true : false;
                DateTime TimeNow = DateTime.Now;
                int CheckStartList = await PunchService.ATCheckPayrollStart(CompID);
                string Department = SelectedDepartment.EntryNum.ToString();
                string IpAddress = (getIpAddress!= "")? getIpAddress : "0";
                int startday = CheckStartList;
                DateTime enddate = DateTime.Now.Date;
                string employeeName = "";
                string Photo = (CanPhoto != null) ? System.Convert.ToBase64String(CanPhoto) : null;
                DateTime startdate = GetDateOfLast(startday - 1);
                var AppSetupList = await PunchService.ATGetAppSetupDefs(CompID);
                string arrivalNot = "0";
                string lateNot = "0";
                string missingNOT = "0";
                string absencesNOT = "0";
                string earlyArrivalNOT = "0";

                if (AppSetupList.Count > 0)
                {
                    foreach (var item in AppSetupList)
                    {

                        if (item.NAME == "ArrivalNotifications")
                        {
                            if (item.ValueAlpha == "1")
                                arrivalNot = "1";
                        }
                        else if (item.NAME == "LateNotification")
                        {
                            if (item.ValueAlpha == "1")
                                lateNot = "1";
                        }
                        else if (item.NAME == "MissingPunchs")
                        {
                            if (!String.IsNullOrEmpty(item.ValueAlpha))
                                missingNOT = item.ValueAlpha;
                        }
                        else if (item.NAME == "Absences")
                        {
                            if (!String.IsNullOrEmpty(item.ValueAlpha))
                                absencesNOT = item.ValueAlpha;
                        }
                        else if (item.NAME == "EarlyArrival")
                        {
                            if (!String.IsNullOrEmpty(item.ValueAlpha))
                                absencesNOT = item.ValueAlpha;
                        }
                    }
                }
                //Check timecard existance 
                var timecard = await PunchService.ATGetTimeCardByDateSpan(CompID, startdate, enddate, EmpEntryNum);
                if (timecard.Count == 0)
                {
                    //No timecard; Create one
                    var InsertResult = await PunchService.ATCreateTimeCard(CompID, EmpId, EmpEntryNum);
                    if (InsertResult > 0)
                    {
                        var CreatedCardList = await PunchService.ATGetLastTimeCardByEmpEntryNum(CompID, EmpEntryNum);

                        if (CreatedCardList.Count > 0)
                        {
                            cardDate = CreatedCardList[0].CardDate;
                            employeeName = CreatedCardList[0].nombre;
                        }
                    }
                }
                else
                {
                    cardDate = timecard[0].carddate;
                    employeeName = timecard[0].nombre;
                }
                var getEmployeeAutoPunch = await PunchService.ATGetEmployeeAutoPunch(CompID, EmpId, EmpEntryNum, TimeNow);
                bool IsAutoPunch = getEmployeeAutoPunch[0].IsAutoPunch;
                bool CanPunch = getEmployeeAutoPunch[0].CanPunch;

                punchData = new PunchData
                {
                    CompId = CompID,
                    EmpId = EmpId,
                    EmployeeEntryNum = System.Convert.ToInt32(EmpEntryNum),
                    PunchTime = TimeNow,
                    CardDate = cardDate,
                    DepartmentId = Department,
                    PunchLocation = "Mobile",
                    PunchType = 1,
                    IsPunchIn = PunchButtonType,
                    UserGuid = userGUID,
                    Photo = Photo,
                    Lat = currentLat,
                    Long = currentLon,
                    IP = IpAddress,
                    PunchTimeString = TimeNow.ToString()
                };

                if (!IsAutoPunch)
                {
                   // await PunchService.ATPunch(punchData);
                }
                else
                {
                    if (CanPunch)
                    {
                       // await PunchService.ATAutoPunch(punchData);
                    }
                }
                var arrayListNotification = new CheckNotifications
                {
                    ArrivalNot = arrivalNot,
                    LateNot = lateNot,
                    DepartmentEntryNum = System.Convert.ToInt32(SelectedDepartment.DepartmentId),
                    EmployeeEntryNum = System.Convert.ToInt32(EmpEntryNum),
                    CompId = CompID,
                    PunchTime = TimeNow,
                    EmployeeName = employeeName,
                    UserGUID = userGUID,
                    AbsentNOT = absencesNOT,
                    MissingNOT = missingNOT,
                    EarlyArrival = earlyArrivalNOT,
                    EmpId = EmpId
                };
                //await PunchService.ATCheckNotifications(arrayListNotification);

                await GetRecentPunches();
                //await GoToPuncher();
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string> { { "CompId", CompID }, { "UserID", EmpId } };
                GetCrashError(exception, properties);
                ShowACToastMessage("Internal Error has ocurred", true);
            }


            Device.BeginInvokeOnMainThread(async () =>
            {
                await ShowNotificationPopup(String.Empty, "Punch Registered Succesfully", String.Empty, NotificationType.Success, nameof(Home), false);
            });

            //await Shell.Current.GoToAsync("..");
            //await Shell.Current.GoToAsync($"//{nameof(Home)}");
            isBusy = false;

           
        }

        //==========================================================================
        //==========================================================================

        public async Task CheckGeoLocation()
        {
            var currentLocation = await GetLocation();
            double latitude2 = currentLocation.Latitude;
            double longitude2 = currentLocation.Longitude;
            string CompID = await SecureStorage.GetAsync("CompId");
            string EmpEntryNum = await SecureStorage.GetAsync("EmployeeEntryNum");
            string EmpId = await SecureStorage.GetAsync("EmpId");
            string GeoSetting = "0";
            Double CompanyLat = 0;
            Double CompanyLong = 0;
            var AppSetupList = await PunchService.ATGetAppSetupDefs(CompID);
            Double MinimunDistance = 0;
            string DistanceMeasure = "";
            int EnforcedCount = 0;
            var passedLocationCheck = true;
            string ErrorMessage = "";

            if (AppSetupList.Count > 0)
            {
                foreach (var item in AppSetupList)
                {

                    if (item.NAME == "Geo")
                    {
                        GeoSetting = item.ValueAlpha;
                    }
                    else if (item.NAME == "CompanyLat")
                    {
                        CompanyLat = System.Convert.ToDouble(item.ValueNumeric);
                    }
                    else if (item.NAME == "CompanyLong")
                    {
                        CompanyLong = System.Convert.ToDouble(item.ValueNumeric);
                    }
                    else if (item.NAME == "GPSDist")
                    {
                        MinimunDistance =System.Convert.ToDouble(item.ValueNumeric);
                        DistanceMeasure = item.ValueAlpha;
                    }
                }
            }
            if (GeoSetting != "0")
            {
                if (Distance(CompanyLat, latitude2, CompanyLong, longitude2, DistanceMeasure) < MinimunDistance)
                {
                    passedLocationCheck = false;
                    ErrorMessage = "La empresa se encuentra fuera del rango configurado en el sistema.";
                    ShowACToastMessage(ErrorMessage, true);
                }
            }
            else
            {
                //Get Locations for this person loop /if enforced check
                var punchLocations = await PunchService.ATGetMobileLocations(CompID, EmpEntryNum, EmpId);

                if (punchLocations.Count > 0)
                {

                    for (var i = 0; i < punchLocations.Count; i++)
                    {
                        if (punchLocations[i].Enforced)
                        {
                            EnforcedCount += 1;
                        }
                        if (Distance(punchLocations[i].Latitude, latitude2, punchLocations[i].Longitude, longitude2, DistanceMeasure) > MinimunDistance)
                        {
                            passedLocationCheck = false;

                            ErrorMessage = "Usted se encuentra fuera del rango configurado en el sistema.";
                            ShowACToastMessage(ErrorMessage, true);
                        }
                    }

                    if (EnforcedCount == 0)
                    {
                        passedLocationCheck = false;
                    }
                }
                else
                {
                    passedLocationCheck = false;
                    ErrorMessage = "Usted no tiene un geolocalización configurada en el sistema.";
                    ShowACToastMessage(ErrorMessage, true);
                }
            }

            if (passedLocationCheck)
            {
                alertLocation = "You are currently in-range";
                iconPinMap = "green_pin_map.png";
                punchEnabled = true;
            }
            else
            {
                alertLocation = "You are not currently in-range";
                iconPinMap = "red_pin_map.png";
                punchEnabled = false;
            }
        }  
        public DateTime GetDateOfLast(int dia)
        {
            Double counter = 0;

            while (System.Convert.ToInt32(DateAdd(DateInterval.Day, counter, DateTime.Now).DayOfWeek) != dia)
            {
                counter = counter - 1;
            }

            return DateAdd(DateInterval.Day, counter, DateTime.Now);
        }
        public static DateTime DateAdd(DateInterval interval, double number, DateTime dateValue)
        {
            DateTime dtm = dateValue;
            switch (interval)
            {
                case DateInterval.Day:
                case DateInterval.DayOfYear:
                case DateInterval.Weekday:
                    dtm = dateValue.AddDays(number);
                    break;
                case DateInterval.Hour:
                    dtm = dateValue.AddHours(number);
                    break;
                case DateInterval.Minute:
                    dtm = dateValue.AddMinutes(number);
                    break;
                case DateInterval.Month:
                    int months = System.Convert.ToInt32(number);
                    dtm = dateValue.AddMonths(months);
                    break;
                case DateInterval.Quarter:
                    int quarters = System.Convert.ToInt32(number);
                    dtm = dateValue.AddMonths(quarters * 3);
                    break;
                case DateInterval.Second:
                    dtm = dateValue.AddSeconds(number);
                    break;
                case DateInterval.WeekOfYear:
                    int weekOfYear = System.Convert.ToInt32(number);
                    dtm = dateValue.AddDays(weekOfYear * 7);
                    break;
                case DateInterval.Year:
                    int years = System.Convert.ToInt32(number);
                    dtm = dateValue.AddYears(years);
                    break;
            }
            return dtm;
        }
        public enum DateInterval
        {
            Year = 0,
            Quarter = 1,
            Month = 2,
            DayOfYear = 3,
            Day = 4,
            WeekOfYear = 5,
            Weekday = 6,
            Hour = 7,
            Minute = 8,
            Second = 9
        } 
        public double Distance(Double latitude1, Double latitude2, Double longitude1, Double longitude2, String measure)
        {

            Double R = 2.093 * Math.Pow(10, 7);//En pies. 
            if (measure == "Feet")
            {
                R = 2.093 * Math.Pow(10, 7); //En pies.
            }
            else
            {
                R = 3959.191; // En millas.
            }

            Double dLat = (latitude2 - latitude1) * Math.PI / 180;
            Double dLon = (longitude2 - longitude1) * Math.PI / 180;
            Double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
            Math.Cos(latitude1 * Math.PI / 180) * Math.Cos(latitude2 * Math.PI / 180) *
            Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            Double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            Double d = R * c;


            return d;
        }
        private string GetDeviceIPAddress()
        {
            string address = String.Empty;
            try
            {
                foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (networkInterface.OperationalStatus == OperationalStatus.Up || networkInterface.OperationalStatus == OperationalStatus.Unknown)
                    {

                        IPInterfaceProperties properties = networkInterface.GetIPProperties();
                        if (properties != null)
                        {
                            if (properties.UnicastAddresses != null && properties.UnicastAddresses.Count > 0)
                            {
                                foreach (var addressInfo in properties.UnicastAddresses)
                                {
                                    if (addressInfo != null && addressInfo.Address != null && addressInfo.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                                    {
                                        address = addressInfo.Address.ToString();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
 
                foreach (var ipAddress in Dns.GetHostAddresses(Dns.GetHostName())) {
                    if (ipAddress != null && !IPAddress.IsLoopback(ipAddress) && ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) {
                        address = ipAddress.ToString();
                    }
                }
 
            }
            return address;
        }
        public async Task<Location> GetLocation()
        {
            Location location = null;
            try
            {
                var errorMessage = "";
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync<LocationPermission>();
                if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        errorMessage = "Usted no tiene permisos de geolocalizacion configurados.";
                        ShowACToastMessage(errorMessage, true);
                    }
                    status = await CrossPermissions.Current.RequestPermissionAsync<LocationPermission>();
                }
                if (status == Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                {

                    location = await Geolocation.GetLastKnownLocationAsync();
                    if (location == null)
                    {
                        location = await Geolocation.GetLocationAsync(new GeolocationRequest
                        {
                            DesiredAccuracy = GeolocationAccuracy.Medium,
                            Timeout = TimeSpan.FromSeconds(5)
                        });
                    }
                }
                else if (status != Plugin.Permissions.Abstractions.PermissionStatus.Unknown)
                {
                    errorMessage = "Permisos de geolocalizacion desconocidos.";
                    ShowACToastMessage(errorMessage, true);
                    CrossPermissions.Current.OpenAppSettings();
                }

                return location;
            }
            catch (Exception ex)
            {
                CrossPermissions.Current.OpenAppSettings();
            }


            return location;
        }  
        public SearchMode SearchMode
        {
            get
            {
                return this.searchMode;
            }
            set
            {
                if (this.searchMode != value)
                {
                    this.searchMode = value;
                    this.OnPropertyChanged();
                }
            }
        }
       
    }
} 