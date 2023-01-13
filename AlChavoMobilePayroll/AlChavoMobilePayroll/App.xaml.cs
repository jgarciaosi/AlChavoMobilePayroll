using AlChavoMobilePayroll.Helpers;
using AlChavoMobilePayroll.Models.SManagement.SecurityManagement;
using AlChavoMobilePayroll.Services;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Crashes;
using System.Globalization;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AlChavoMobilePayroll
{
    public partial class App : Application
    {
        public static SECSystem SystemDflts { get; set; }

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NzUyNDg0QDMyMzAyZTMzMmUzMEYzbzZGdFV6WnN6bHZ4SEVhb1AralZhNXRONXBpK0FtVnNOSjJzUnFXUXc9");

            InitializeComponent();

            SetCultureToUSEnglish();

            DependencyService.Register<MockDataStore>();

            DependencyService.Register<LoginService>();

            DependencyService.Register<UserService>();

            DependencyService.Register<CompanyService>();

            DependencyService.Register<PunchService>();

            DependencyService.Register<PermissionService>();

            DependencyService.Register<CheckRegisterService>();

            DependencyService.Register<AttendanceService>();

            DependencyService.Register<EmployeeService>();

            SystemDflts = new SECSystem();


            MainPage = new AppShell();
        }



        public bool DoBack
        {
            get
            {
                return MainPage.Navigation.NavigationStack.Count > 1;
            }
        }

        protected override void OnStart()
        {
            OnResume();
            AppCenter.Start("ios=d1caec54-39e8-472c-9656-e56af83ba0a7;" + "android=ecc42b58-f548-4a37-b3df-0cbccbad6489;", typeof(Crashes));
        }

        protected override void OnSleep()
        {
            TheTheme.SetTheme();
            RequestedThemeChanged -= App_RequestedThemeChanged;
        }

        protected override void OnResume()
        {
            SetCultureToUSEnglish();
            TheTheme.SetTheme();
            RequestedThemeChanged += App_RequestedThemeChanged;

        }

        private void App_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                TheTheme.SetTheme();
            });
        }

        private void SetCultureToUSEnglish()
        {

            CultureInfo englishUSCulture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = englishUSCulture;
        }

    }
}
