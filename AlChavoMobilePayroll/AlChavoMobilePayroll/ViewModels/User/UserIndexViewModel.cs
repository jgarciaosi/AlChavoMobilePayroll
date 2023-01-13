using AlChavoMobilePayroll.Helpers;
using AlChavoMobilePayroll.Models.SManagement.Company;
using AlChavoMobilePayroll.Views.Company;
using AlChavoMobilePayroll.Views.SManagement;
using AlChavoMobilePayroll.Views.User;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AlChavoMobilePayroll.ViewModels.User
{
    [QueryProperty(nameof(name), nameof(name))]
    [QueryProperty(nameof(email), nameof(email))]
    public class UserIndexViewModel : Common.BaseViewModel
    {
        private int selectedTheme;
        private bool defaultTheme;
        private bool darkTheme;
        private bool lightTheme;
        #region Properties
        private string Name { get; set; }
        private string Email { get; set; }

        public string name { get => Name; set { Name = value; OnPropertyChanged(); } }
        public string email { get => Email; set { Email = value; OnPropertyChanged(); } }

        public bool DefaultTheme { get => defaultTheme; set { defaultTheme = value; OnPropertyChanged(); } }
        public bool DarkTheme { get => darkTheme; set { darkTheme = value; OnPropertyChanged(); } }
        public bool LightTheme { get => lightTheme; set { lightTheme = value; OnPropertyChanged(); } }



        public ObservableCollection<string> ThemeOptions { get; } = new ObservableCollection<string>();
        public int SelectedTheme { get => selectedTheme; set { selectedTheme = value; OnPropertyChanged(); } }
        #endregion

        #region Commands
        public IAsyncCommand GoToUserDetailCommand { get; set; }
        public IAsyncCommand GoToCompanyDetailCommand { get; set; }
        public IAsyncCommand GoToLoginCommand { get; set; }

        public IAsyncCommand SelectedCompanyChangedCommand { get; set; }
        public ICommand SelectedMethodCommand { get; set; }
        #endregion


        public UserIndexViewModel()
        {
            isBusy = true;

            Title = "User Preferences";

            GoToUserDetailCommand = new AsyncCommand(GoToUserDetail);
            GoToCompanyDetailCommand = new AsyncCommand(GoToCompanyDetail);
            GoToLoginCommand = new AsyncCommand(GoToLogin);

            SelectedMethodCommand = new Command((item) => ChangeThemeBySelecctedMethod(item));

            InitializeData();

            isBusy = false;
        }

      
        private void ChangeThemeBySelecctedMethod(object item)
        {
            isBusy = true;

            var SelectedItem = item as Xamarin.Forms.RadioButton;
            switch (SelectedItem.Value)
            {
                case "System":
                    Settings.Theme = 0;
                    break;
                case "Light":
                    Settings.Theme = 1;
                    break;
                case "Dark":
                    Settings.Theme = 2;
                    break;
            }

            TheTheme.SetTheme();

            isBusy = false;
        }

        public async Task GoToUserDetail()
        {
            if (SECAuthorizeAction(nameof(UserDetail), Actions.View))
                await Shell.Current.GoToAsync($"{nameof(UserDetail)}");
        }

        public async Task GoToCompanyDetail()
        {
            if (SECAuthorizeAction(nameof(CompanyDetail), Actions.View))
                await Shell.Current.GoToAsync($"{nameof(CompanyDetail)}?{nameof(this.name)}={Name}&{nameof(this.email)}={Email}");
        
        
        }

        public async Task GoToLogin()
        {
            SecureStorage.RemoveAll();

            await Shell.Current.GoToAsync($"/{nameof(Login)}");

        }

        public async void InitializeData()
        {
            var compId = await SecureStorage.GetAsync("CompId");

            switch (Settings.Theme)
            {
                case 0:
                    DefaultTheme = true;
                    LightTheme = false;
                    DarkTheme = false;
                    break;
                case 1:
                    DefaultTheme = false;
                    LightTheme = true;
                    DarkTheme = false;
                    break;
                case 2:
                    DefaultTheme = false;
                    LightTheme = false;
                    DarkTheme = true;
                    break;
            }
        }


        public async Task<List<CompaniesByUserResponse>> GetCompaniesByUser(string id)
        {
            if (string.IsNullOrEmpty(id))
                return default;

            var data = await CompanyService.GetByUserIdMobileRestricted(id).ConfigureAwait(false);
            return data;
        }
    }
}
