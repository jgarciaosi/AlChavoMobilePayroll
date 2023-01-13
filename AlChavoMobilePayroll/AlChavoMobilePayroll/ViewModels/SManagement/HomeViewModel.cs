using AlChavoMobilePayroll.Models;
using AlChavoMobilePayroll.Models.Attendance.Punches;
using AlChavoMobilePayroll.Models.CheckRegister;
using AlChavoMobilePayroll.Models.SManagement.Company;
using AlChavoMobilePayroll.Models.SManagement.User;
using AlChavoMobilePayroll.ViewModels.CheckRegister;
using AlChavoMobilePayroll.ViewModels.User;
using AlChavoMobilePayroll.Views.CheckRegister;
using AlChavoMobilePayroll.Views.SManagement;
using AlChavoMobilePayroll.Views.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Documents.Common.Model;
using Telerik.XamarinForms.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AlChavoMobilePayroll.ViewModels.SManagement
{
    public class HomeViewModel : Common.BaseViewModel
    {
        private List<CompaniesByUserResponse> CompanyList { get; set; }
        private CompaniesByUserResponse SelectedCompany { get; set; }
        private UserInfoResponse UserModel { get; set; }
        private SearchMode searchMode;
        public List<CompaniesByUserResponse> companyList { get => CompanyList; set { CompanyList = value; OnPropertyChanged(); } }
        public List<NameValuePair> companyDemoList { get; set; }
        public CompaniesByUserResponse selectedCompany { get => SelectedCompany; set { SelectedCompany = value; OnPropertyChanged(); } }
        public UserInfoResponse userModel { get => UserModel; set { UserModel = value; OnPropertyChanged(); } }
        public IAsyncCommand GoToPunch { get; set; }

        public ATGetLastPunchResponse getLastPunchResponse { get => GetLastPunchResponse; set { GetLastPunchResponse = value; OnPropertyChanged(); } }
        private ATGetLastPunchResponse GetLastPunchResponse { get; set; }

        public bool IsPunchIn { get => isPunchIn; set { isPunchIn = value; OnPropertyChanged(); } }
        private bool isPunchIn { get; set; }

        public bool IsPunchOut { get => isPunchOut; set { isPunchOut = value; OnPropertyChanged(); } }
        private bool isPunchOut { get; set; }

        public bool isVisibleGross { get => IsVisibleGross; set { IsVisibleGross = value; OnPropertyChanged(); } }
        private bool IsVisibleGross { get; set; }

        public bool isVisibleNet { get => IsVisibleNet; set { IsVisibleNet = value; OnPropertyChanged(); } }
        private bool IsVisibleNet { get; set; }

        public bool isVisibleHiddenGross { get => IsVisibleHiddenGross; set { IsVisibleHiddenGross = value; OnPropertyChanged(); } }
        private bool IsVisibleHiddenGross { get; set; }

        public bool isVisibleHiddenNet { get => IsVisibleHiddenNet; set { IsVisibleHiddenNet = value; OnPropertyChanged(); } }
        private bool IsVisibleHiddenNet { get; set; }

        public bool IsVisibleHoursTitle { get => isVisibleHoursTitle; set { isVisibleHoursTitle = value; OnPropertyChanged(); } }
        private bool isVisibleHoursTitle { get; set; }

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

        #region Commands

        public IAsyncCommand<object> SelectedCompanyChangedCommand { get; set; }
        public IAsyncCommand GoToUserCommand { get; set; }
        public IAsyncCommand GoToOverDueCommand { get; set; }
        public IAsyncCommand GoToOpenCommand { get; set; }
        public IAsyncCommand GoToScheduleCommand { get; set; }
        public IAsyncCommand GoToAddBillCommand { get; set; }
        public IAsyncCommand GoToAddBillPlusCommand { get; set; }

        public Xamarin.CommunityToolkit.ObjectModel.IAsyncCommand<object> NetVisibilityCommand { get; set; }

        public Xamarin.CommunityToolkit.ObjectModel.IAsyncCommand<object> GrossVisibilityCommand { get; set; }

        private IReadOnlyCollection<NameValuePair> businessOverview;
        public IReadOnlyCollection<NameValuePair> BusinessOverview { get => businessOverview; private set => SetProperty(ref businessOverview, value); }

        private ICommand _tapChartCommand;
        public ICommand TapChartCommand => _tapChartCommand = new AsyncCommand(OnChartTappedAsync);

        #endregion

        public HomeViewModel()
        {

            IsPunchIn = false;
            IsPunchOut = false;
            IsVisibleHoursTitle = false;
            SelectedCompanyChangedCommand = new AsyncCommand<object>((item) => GetSelectedCompany(item));
            NetVisibilityCommand = new AsyncCommand<object>((item) => SetNetVisibility(item));
            GrossVisibilityCommand = new AsyncCommand<object>((item) => SetGrossVisibility(item));
            GoToUserCommand = new AsyncCommand(GoToUser);
            GoToPunch = new AsyncCommand(GoToPuncher);

        }


        public async void InitializeData()
        {

            isBusy = true;
            await LoadHomeInformation();
            isBusy = false;
        }

        private async Task LoadHomeInformation()
        {
                var CompID = await SecureStorage.GetAsync("CompId");
                var userGUID = await SecureStorage.GetAsync("UserGUID");
                var EmpId = await SecureStorage.GetAsync("EmpId");
            try
            {

                companyList = await GetCompaniesByUser(userGUID).ConfigureAwait(false);
             
                selectedCompany = companyList.FirstOrDefault(i => i.CompID.ToLowerInvariant() == CompID.ToLowerInvariant());

                userModel = await GetUserInfo(userGUID).ConfigureAwait(false);

                checkRegisterLastResponse = await GetCheckRegisterLast(new GetCheckRegisterDetailRequest() { Compid = CompID, EmployeeId = EmpId }).ConfigureAwait(false);

                getLastPunchResponse = await ATGetMobileLastPunch(CompID, EmpId);

                isVisibleGross = false;
                isVisibleHiddenGross = true;

                isVisibleNet = false;
                isVisibleHiddenNet = true;



            }
            catch (Exception ex)
            {
                var properties = new Dictionary<string, string> { { "CompId", CompID }, { "UserID", userGUID } };
                GetCrashError(ex, properties);
            }
        }

        public async Task GoToPuncher()
        {
            //  var isAvailable = await CrossFingerprint.Current.IsAvailableAsync();

            //  if (!isAvailable) {
            //   if (SECAuthorizeAction(nameof(Punch), Actions.View))
            //      await Shell.Current.GoToAsync($"{nameof(Punch)}");

            // }
            //var result = await CrossFingerprint.Current.AuthenticateAsync(new
            // AuthenticationRequestConfiguration("Biometrics", "Use your biometrics, please"));
            // if (result.Authenticated) {
            var PAMobileCanPunchYN = await SecureStorage.GetAsync("PAMobileCanPunchYN");
            if (SECAuthorizeAction(nameof(Punch), Actions.View) && (PAMobileCanPunchYN.Equals("0")?false:true))
                await Shell.Current.GoToAsync($"{nameof(Punch)}");
          
            else
                ShowACToastMessage("User doesn´t have permition to Punch", true);
            // }
        }

        async Task OnChartTappedAsync()
        {
            isBusy = true;

            GetAllResponse checkRegisterDetailData = new GetAllResponse();
            checkRegisterDetailData.CheckNumber = checkRegisterLastResponse.CheckNumber;
            if (checkRegisterLastResponse.CheckHours != null)
            {
                checkRegisterDetailData.CheckHours = (decimal)checkRegisterLastResponse.CheckHours;
            }
            else
            {
                checkRegisterDetailData.CheckHours = null;
            }

            checkRegisterDetailData.CheckDate = checkRegisterLastResponse.CheckDate;
            checkRegisterDetailData.CompId = checkRegisterLastResponse.CompId;
            checkRegisterDetailData.EmpId = checkRegisterLastResponse.EmpId;

            var model = JsonConvert.SerializeObject(checkRegisterDetailData);

            await Shell.Current.GoToAsync($"/{nameof(CheckRegisterDetail)}?{nameof(CheckRegisterDetailViewModel.content)}={model}");



            isBusy = false;
        }

        private async Task SetNetVisibility(object item)
        {
            ImageButton imgButton = (ImageButton)item;
            if (imgButton.StyleId.ToString() == "HiddenIconNet")
            {
                isVisibleNet = true;
                isVisibleHiddenNet = false;
            }
            else
            {
                isVisibleNet = false;
                isVisibleHiddenNet = true;
            }

        }

        private async Task SetGrossVisibility(object item)
        {

            ImageButton imgButton = (ImageButton)item;
            if (imgButton.StyleId.ToString() == "HiddenIconGross")
            {
                isVisibleGross = true;
                isVisibleHiddenGross = false;
            }
            else
            {
                isVisibleGross = false;
                isVisibleHiddenGross = true;
            }

        }

        public async Task GoToCheckRegisterDetail()
        {
            if (SECAuthorizeAction(nameof(Punch), Actions.View))
                await Shell.Current.GoToAsync($"{nameof(Punch)}");
        }


        public async Task GoToUser()
        {
            if (SECAuthorizeAction(nameof(UserIndex), Actions.View))
                await Shell.Current.GoToAsync($"{nameof(UserIndex)}?{nameof(UserIndexViewModel.name)}={UserModel.Name}&{nameof(UserIndexViewModel.email)}={UserModel.Email}").ConfigureAwait(false);
        }


        public async Task<UserInfoResponse> GetUserInfo(string id)
        {
            if (string.IsNullOrEmpty(id))
                return default;

            var data = await UserService.GetUserInfo(id).ConfigureAwait(false);
            return data;
        }
        public async Task<List<CompaniesByUserResponse>> GetCompaniesByUser(string id)
        {
            if (string.IsNullOrEmpty(id))
                return default;

            var data = await CompanyService.GetByUserIdMobileRestricted(id).ConfigureAwait(false);
            return data;
        }


        private async Task GetSelectedCompany(object item)
        {
            var SelectedItem = item as RadComboBox;
            selectedCompany = SelectedItem.SelectedItem as CompaniesByUserResponse;

            if (selectedCompany != null)
            {
                var compId = await SecureStorage.GetAsync("CompId");

                if (!selectedCompany.CompID.Equals(compId))
                {

                    isBusy = true;

                    var hasPermission = await SECSetUserPreferences(selectedCompany.CompID);


                    if (!hasPermission)
                    {
                        isBusy = false;
                        await Shell.Current.GoToAsync($"{nameof(Login)}").ConfigureAwait(true);
                        ShowACToastMessage("User Don´t have access permissions", true);
                    }
                    else
                    {
                        await LoadHomeInformation();
                        isBusy = false;
                    }

                }
            }
        }




        public ObservableCollection<NameValuePair> NameValuePairs { get; set; }


        public GetCheckRegisterDetailResponse checkRegisterLastResponse { get => CheckRegisterLastResponse; set { CheckRegisterLastResponse = value; OnPropertyChanged(); } }
        private GetCheckRegisterDetailResponse CheckRegisterLastResponse { get; set; }

        public async Task<ATGetLastPunchResponse> ATGetMobileLastPunch(string compId, string empId)
        {
            var data = await AttendanceService.ATGetMobileLastPunch(compId, empId).ConfigureAwait(false);


            if (data != null)
            {
                IsPunchOut = data.isPunchOut;
                IsPunchIn = data.isPunchIn;
            }


            return data;
        }

        public async Task<GetCheckRegisterDetailResponse> GetCheckRegisterLast(GetCheckRegisterDetailRequest checkRegisterLastRequest)
        {
            var data = await CheckRegisterService.GetSSPCheckRegisterLast(checkRegisterLastRequest).ConfigureAwait(false);

            if (data != null && data.CheckHours != null)
            {
                IsVisibleHoursTitle = true;
            }

            return data;
        }

        /////////////////////////////////////
        public ObservableCollection<Model> Data { get; set; }

        public ObservableCollection<Model> data { get => Data; set { Data = value; OnPropertyChanged(); } }
        //////////////////////////////////////
        ///
        public class Model
        {
            public string Month { get; set; }

            public double Target { get; set; }

            public Model(string xValue, double yValue)
            {
                Month = xValue;
                Target = yValue;
            }
        }






    }
}
