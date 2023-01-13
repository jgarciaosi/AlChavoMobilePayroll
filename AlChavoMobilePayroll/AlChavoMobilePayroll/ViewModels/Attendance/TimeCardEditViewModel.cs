using AlChavoMobilePayroll.Models;
using AlChavoMobilePayroll.Models.Attendance;
using AlChavoMobilePayroll.Models.Attendance.Punches;
using AlChavoMobilePayroll.Models.SManagement.Company;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Telerik.XamarinForms.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using static AlChavoMobilePayroll.ViewModels.SManagement.PunchViewModel;

namespace AlChavoMobilePayroll.ViewModels.Attendance
{
    [QueryProperty(nameof(content), nameof(content))]
    public class TimeCardEditViewModel : Common.BaseViewModel
    {
        private string Content { get; set; }
        public string content
        {
            get => Content;
            set
            {
                Content = value;
                ATGetTimeCardByData(value);
                OnPropertyChanged();
            }
        }
        public DateTime cardDate;
        public PunchData punchData;
        public ATGetTimeCardByIdResponse responseTimecard { get; set; }
        public bool isFirstTime { get; set; }
        public ObservableCollection<ATGetEditPunchesResponse> punchesList { get => PunchesList; set { PunchesList = value; OnPropertyChanged(); } }
        public Xamarin.CommunityToolkit.ObjectModel.IAsyncCommand<object> IAddPunchCommand { get; set; }
        public Xamarin.CommunityToolkit.ObjectModel.IAsyncCommand<object> IDeletePunchCommand { get; set; }
        public Xamarin.CommunityToolkit.ObjectModel.IAsyncCommand<object> PeriodsChangedCommand { get; set; }
        private ObservableCollection<ATGetEditPunchesResponse> PunchesList { get; set; }
        private List<DepartmentByCompaniesResponse> DepartmentList { get; set; }
        public List<DepartmentByCompaniesResponse> departmentList { get => DepartmentList; set { DepartmentList = value; OnPropertyChanged(); } }
        public IAsyncCommand<object> SelectedDepartmentCommand { get; set; }
        public Xamarin.CommunityToolkit.ObjectModel.IAsyncCommand<object> SelectedPunchTimeCommand { get; set; }
        public Xamarin.CommunityToolkit.ObjectModel.IAsyncCommand<object> SelectedPunchInOutCommand { get; set; }

        private DepartmentByCompaniesResponse SelectedDepartment { get; set; }
        public DepartmentByCompaniesResponse selectedDepartment { get => SelectedDepartment; set { SelectedDepartment = value; OnPropertyChanged(); } }
        public TimeCardEditViewModel()
        {
            Title = "Hours";
            isFirstTime = true;
            IAddPunchCommand = new Xamarin.CommunityToolkit.ObjectModel.AsyncCommand<object>((item) => CreatePunch(item));
            IDeletePunchCommand = new Xamarin.CommunityToolkit.ObjectModel.AsyncCommand<object>((item) => DeletePunch(item));
            
        }

        private void LoadAttendanceTimeCard()
        {

            ATGetEditPunchesResponse itemNew = new ATGetEditPunchesResponse();

            departmentList = responseTimecard.departmentList;

            if (responseTimecard.IsPunchInFlagForEdit)
            {
                itemNew.PunchID = (int)responseTimecard.PunchInID;
                itemNew.PunchDateTime = new DateTime(responseTimecard.PunchInDate.Value.Year,
                    responseTimecard.PunchInDate.Value.Month,
                    responseTimecard.PunchInDate.Value.Day,
                    responseTimecard.OrigPunchInHour.Value.Hour,
                    responseTimecard.OrigPunchInHour.Value.Minute,
                    responseTimecard.OrigPunchInHour.Value.Second);
                itemNew.PunchInIndex = 0;

                selectedDepartment = departmentList.Find(x => x.DepartmentId == responseTimecard.Department);

            }
            else
            {
                itemNew.PunchID = (int)responseTimecard.PunchOutID;
                itemNew.PunchDateTime = new DateTime(responseTimecard.PunchOutDate.Value.Year,
                responseTimecard.PunchOutDate.Value.Month,
                responseTimecard.PunchOutDate.Value.Day,
                responseTimecard.OrigPunchOutHour.Value.Hour,
                responseTimecard.OrigPunchOutHour.Value.Minute,
                responseTimecard.OrigPunchOutHour.Value.Second);
                itemNew.PunchInIndex = 1;

                selectedDepartment = departmentList.Find(x => x.DepartmentId == responseTimecard.DptOut);
            }



            punchesList = new ObservableCollection<ATGetEditPunchesResponse>();
            punchesList.Add(itemNew);


            SelectedDepartmentCommand = new AsyncCommand<object>((item) => GetSelectedDeparment(item));
            SelectedPunchTimeCommand = new AsyncCommand<object>((item2) => GetSelectedPunchTime(item2));
            SelectedPunchInOutCommand = new AsyncCommand<object>((item3) => GetSelectedPunchInOut(item3));
        }

        private async Task GetSelectedDeparment(object item)
        {
            StackLayout SelectedItem = (StackLayout)item;
            Label selectedPunchIDLabel = (Label)SelectedItem.Children[0];
            RadComboBox selectedDeparmentCombo = (RadComboBox)SelectedItem.Children[1];
            DepartmentByCompaniesResponse selectedItemCombo = (DepartmentByCompaniesResponse)selectedDeparmentCombo.SelectedItem;
            await ATUpdateMobilePunches(Int32.Parse(selectedPunchIDLabel.Text), selectedItemCombo.EntryNum, null, null, 1);



        }

        private async Task GetSelectedPunchTime(object item2)
        {
            if (!isFirstTime)
            {
                Grid SelectedItem = (Grid)item2;
                Label selectedPunchIDLabel = (Label)SelectedItem.Children[0];
                RadDateTimePicker selectedRadDateTime = (RadDateTimePicker)SelectedItem.Children[3];
                //bool modifiedRegister = false;

                //if (responseTimecard.IsPunchInFlagForEdit)
                //{
                //    if (selectedRadDateTime.Date.Value.Date != responseTimecard.PunchInDate)
                //    {
                isBusy = true;
                if (selectedPunchIDLabel.Text != null)
                {
                    if (selectedPunchIDLabel.Text.ToString().All(char.IsNumber))
                    {
                        await ATUpdateMobilePunches(Int32.Parse(selectedPunchIDLabel.Text), null, selectedRadDateTime.Date, null, 2);
                    }
                }
                isBusy = false;
                //modifiedRegister = true;
                //    }

                //    if ((selectedRadDateTime.Date.Value.Hour != responseTimecard.PunchInHour.Value.Hour) ||
                //            (selectedRadDateTime.Date.Value.Minute != responseTimecard.PunchInHour.Value.Minute) ||
                //            (selectedRadDateTime.Date.Value.Year != responseTimecard.PunchInHour.Value.Year))
                //    {
                //        if (modifiedRegister == false)
                //        {
                //            await ATUpdateMobilePunches(Int32.Parse(selectedPunchIDLabel.Text), null, selectedRadDateTime.Date, null, 2);
                //        }
                //    }


                //}
                //else
                //{

                //    if (selectedRadDateTime.Date.Value.Date != responseTimecard.PunchInDate)
                //    {
                //        await ATUpdateMobilePunches(Int32.Parse(selectedPunchIDLabel.Text), null, selectedRadDateTime.Date, null, 2);
                //        modifiedRegister = true;
                //    }

                //    if ((selectedRadDateTime.Date.Value.Hour != responseTimecard.PunchOutHour.Value.Hour) ||
                //            (selectedRadDateTime.Date.Value.Minute != responseTimecard.PunchOutHour.Value.Minute) ||
                //            (selectedRadDateTime.Date.Value.Year != responseTimecard.PunchOutHour.Value.Year))
                //    {
                //        if (modifiedRegister == false)
                //        {
                //            await ATUpdateMobilePunches(Int32.Parse(selectedPunchIDLabel.Text), null, selectedRadDateTime.Date, null, 2);
                //        }
                //    }

                //}

                //}


            }
        }

        private async Task GetSelectedPunchInOut(object item3)
        {
            if (!isFirstTime)
            {
                Grid SelectedItem = (Grid)item3;
                Label selectedPunchIDLabel = (Label)SelectedItem.Children[0];
                RadSegmentedControl selectedPunchInOut = (RadSegmentedControl)SelectedItem.Children[4];
                bool flagPunch;
                if (selectedPunchInOut.SelectedIndex == 0)
                {
                    flagPunch = true;
                }
                else
                {
                    flagPunch = false;
                };

                if (selectedPunchIDLabel.Text != null)
                {
                    if (selectedPunchIDLabel.Text.ToString().All(char.IsNumber))
                    {                        
                        await ATUpdateMobilePunches(Int32.Parse(selectedPunchIDLabel.Text), null, null, flagPunch, 3);
                    }
                }



            }
            isFirstTime = false;


        }


        private async Task CreatePunch(object item)
        {
            //var infos = TimeZoneInfo.GetSystemTimeZones();
            //var puertoRicanTime = TimeZoneInfo.ConvertTime(DateTime.Now,
            //     TimeZoneInfo.FindSystemTimeZoneById("America/Puerto_Rico"));



            isBusy = true;
            try
            {
                ATGetEditPunchesResponse itemNew = new ATGetEditPunchesResponse();
                
                itemNew.PunchDateTime = DateTime.Now.ToLocalTime(); 
                itemNew.PunchInIndex = 0;

                var punchIDResponse = await DoPunch(itemNew.PunchInIndex == 1 ? true : false);
                itemNew.PunchID = int.Parse(punchIDResponse);
                punchesList.Add(itemNew);

            }
            catch (Exception ex)
            {
                string a = ex.Message;

            }

            isBusy = false;


        }

        private async Task DeletePunch(object item)
        {
            string CompID = await SecureStorage.GetAsync("CompId");
            string userGUID = await SecureStorage.GetAsync("UserGUID");
            isBusy = true;
            PunchData punchDeleteData = new PunchData();
            punchDeleteData.PunchID = ((ATGetEditPunchesResponse)item).PunchID;
            punchDeleteData.CompId = CompID;
            punchDeleteData.UserGuid = userGUID;
            await DeletePunchById(punchDeleteData);
            punchesList.Remove((ATGetEditPunchesResponse)item);
            isBusy = false;

        }

        public async Task DeletePunchById(PunchData paramPunchData)
        {
            await PunchService.ATDeletePunchByID(paramPunchData).ConfigureAwait(false);

        }

        public async Task UpdateMobilePunches(PunchData punchData)
        {
            await PunchService.ATUpdateMobilePunches(punchData).ConfigureAwait(false);

        }


        public async void ATGetTimeCardByData(string data)
        {
            responseTimecard = JsonConvert.DeserializeObject<ATGetTimeCardByIdResponse>(data);
            LoadAttendanceTimeCard();
            //await LoadDepartmentInformation();


        }



        private async Task LoadDepartmentInformation()
        {
            var CompID = await SecureStorage.GetAsync("CompId");

            departmentList = await GetDepartmentByCompaines(CompID).ConfigureAwait(false);
            selectedDepartment = departmentList[1];
        }

        public async Task<List<DepartmentByCompaniesResponse>> GetDepartmentByCompaines(string id)
        {
            if (string.IsNullOrEmpty(id))
                return default;

            var data = await PunchService.GetDepartmentByCompId(id).ConfigureAwait(false);
            return data;
        }

        public async Task<GetRecordLicenseResponse> ATUpdateMobilePunches(int punchID, Nullable<int> departmentEntryNumID, Nullable<DateTime> punchTime, Nullable<bool> isPunchIn, int UpdateType)
        {
            var data = await AttendanceService.ATUpdateMobilePunches(punchID, departmentEntryNumID, punchTime, isPunchIn, UpdateType).ConfigureAwait(false);
            return data;

        }

        //Punches
        public static string GetIPAddress()
        {
            var IpAddress = Dns.GetHostAddresses(Dns.GetHostName()).FirstOrDefault();

            if (IpAddress != null)
            {
                return IpAddress.ToString();
            }

            return null;
        }
        public async Task<Location> GetLocation()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            if (location == null)
            {
                location = await Geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(5)
                });
            }

            return location;
        }

        public DateTime GetDateOfLast(int dia)
        {
            Double counter = 0;

            while (Convert.ToInt32(DateAdd(DateInterval.Day, counter, DateTime.Now).DayOfWeek) != dia)
            {
                counter = counter - 1;
            }

            return DateAdd(DateInterval.Day, counter, DateTime.Now);
        }

        private async Task<string> DoPunch(bool PunchInOrOut)
        {
            Location currentLocation = await GetLocation();
            var currentLat = currentLocation.Latitude;
            var currentLon = currentLocation.Longitude;
            string CompID = await SecureStorage.GetAsync("CompId");
            string userGUID = await SecureStorage.GetAsync("UserGUID");
            string EmpEntryNum = await SecureStorage.GetAsync("EmployeeEntryNum");
            string EmpId = await SecureStorage.GetAsync("EmpId");
            DateTime TimeNow = DateTime.Now;
            int CheckStartList = await PunchService.ATCheckPayrollStart(CompID);
            string Department = selectedDepartment.DepartmentId;
            string IpAddress = GetIPAddress();
            int startday = CheckStartList;
            DateTime enddate = DateTime.Now.Date;
            string employeeName = "";
            DateTime startdate = GetDateOfLast(startday - 1);

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
                EmployeeEntryNum = Convert.ToInt32(EmpEntryNum),
                PunchTime = TimeNow,
                CardDate = cardDate,
                DepartmentId = Department,
                PunchLocation = "Mobile",
                PunchType = 1,
                IsPunchIn = PunchInOrOut,
                UserGuid = userGUID,
                Photo = null,
                Lat = currentLat,
                Long = currentLon,
                IP = IpAddress
            };
            Services.Response response;
            response = await PunchService.ATPunchEdit(punchData);
            return response.Status;

        }

    }


}
