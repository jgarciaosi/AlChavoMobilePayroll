using AlChavoMobilePayroll.Helpers;
using AlChavoMobilePayroll.Models.SManagement.Login;
using AlChavoMobilePayroll.Services;
using AlChavoMobilePayroll.Views.SManagement;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AlChavoMobilePayroll.ViewModels.SManagement
{
    public class LoginViewModel : Common.BaseViewModel
    {

        private LoginRequest authUser = new LoginRequest();
        private bool rememberUser;
        public LoginRequest AuthUser { get => authUser; set { authUser = value; OnPropertyChanged(); } } //Login User Information
        public bool RememberUser { get => rememberUser; set { rememberUser = value; OnPropertyChanged(); } }//bool Switch to save UserName
        public string AuthResult { get; set; }


        public IAsyncCommand LoginCommand { get; set; }
        public ICommand RememberUserCommand { get; set; }
        public ICommand GotoForgotPassCommand { get; set; }
        public ICommand ClosePopupCommand { get; set; }
        public ICommand GoToAlchavoCommand { get; set; }
        public LoginResponse LoginResponse { get; set; }


        public LoginViewModel()
        {
            TheTheme.SetTheme();

            AuthUser.UserName = Preferences.Get("UsrName", "");
            RememberUser = Preferences.Get("UsrNameChk", false);

            LoginCommand = new AsyncCommand(OnLoginClicked, allowsMultipleExecutions: false);

            RememberUserCommand = new Command((item) => RememberUserToogledCmd(item));

            GotoForgotPassCommand = new Command(GoToForgotPassword);

            GoToAlchavoCommand = new Command(GoToAlchavo);
        }

        private async void GoToForgotPassword(object obj)
        {
            await Browser.OpenAsync(new Uri("https://ac3.alchavo.com/Account/ResetPassword.aspx"), BrowserLaunchMode.SystemPreferred);
        }

        private async void GoToAlchavo(object obj)
        {
            await Browser.OpenAsync(new Uri("https://www.alchavo.com/"), BrowserLaunchMode.SystemPreferred);
        }

        private void RememberUserToogledCmd(object item)
        {
            var SetUser = Convert.ToBoolean(item);
            RememberUser = SetUser;
            SavePreferences();
        }

        private async Task OnLoginClicked()
        {

            var Validator = new LoginRequestValidator();
            ValidationResult result = Validator.Validate(AuthUser);

            if (result.IsValid)
            {

                isBusy = true;
                await AuthLogin().ConfigureAwait(true);
                isBusy = false;


                switch (AuthResult)
                {
                    case "1":
                        await Shell.Current.GoToAsync($"///{nameof(Home)}");
                        break;
                    case "2":
                        String msj = "This user does not have access permition to the AlChavo PA Module. For more information, please contact customerservice@osipr.com or visit AlChavo.com";
                        await ShowNotificationPopup("Important", msj, "Go to AlChavo.com", NotificationType.Information, "https://www.alchavo.com/", false);
                        break;
                    case "3":
                        ShowACToastMessage("Access Denied, Wrong User of Password", true);
                        break;
                }

            }
            else
            {
                string ErrorMessage = string.Empty;
                foreach (var error in result.Errors)
                {
                    ErrorMessage = error.ErrorMessage;
                }

                ShowACToastMessage(ErrorMessage, true);

            }


        }

        private async Task AuthLogin()
        {
         //  AuthUser.Password = "Ac30s1m1sl0ginma5t3r1112023";


            String CompID = String.Empty;

            try
            {
                SavePreferences();
                AuthUser.CompId = Preferences.Get("CompId", "");//Obtiene la ultima compañia en la que se Logeo el user
                LoginResponse = await LoginService.authenticate(AuthUser).ConfigureAwait(true);

                if (!string.IsNullOrEmpty(LoginResponse.Token))
                {
                    await SecureStorage.SetAsync("CompId", LoginResponse.CompId.ToString());

                    CompID = LoginResponse.CompId.ToString();
                    await SecureStorage.SetAsync("AuthTkn", LoginResponse.Token.ToString());
                    await SecureStorage.SetAsync("UserGUID", LoginResponse.AC30UserID.ToString());
                    await SecureStorage.SetAsync("UserID", LoginResponse.AC20UserID.ToString());

                    if (LoginResponse.EmpId == null)
                    {
                        string ErrorMessage = "The user doesn't have Self Service Portal";
                        ShowACToastMessage(ErrorMessage, true);
                        return;
                    }
                    await SecureStorage.SetAsync("EmpId", LoginResponse.EmpId.ToString());
                    await SecureStorage.SetAsync("EmployeeEntryNum", LoginResponse.EmployeeEntryNum.ToString());
                    await SecureStorage.SetAsync("PAMobileOptionalFieldsYN", LoginResponse.PAMobileEditPunchYN.ToString());
                    await SecureStorage.SetAsync("PAMobileEditPunchYN", LoginResponse.PAMobileEditPunchYN.ToString());
                    await SecureStorage.SetAsync("PAMobileCanPunchYN", LoginResponse.PAMobileCanPunchYN.ToString());
                    await SecureStorage.SetAsync("DepartmentId", LoginResponse.DepartmentId.ToString());

                    await SecureStorage.SetAsync("CompId", CompID);
                    Preferences.Set("CompId", CompID);
                    AuthResult = "1";

                    //var hasPermission = await SECSetUserPreferences(CompID).ConfigureAwait(true);
                    //if (hasPermission)
                    //    AuthResult = "1";  // AQUI AUTENTICA
                    //else
                    //    AuthResult = "2";  //No Tiene Acceso a AP y abre Popup

                }
                else
                {
                    AuthResult = "3";// No tiene acceso por error de autenticacion, envia Status
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                //var properties = new Dictionary<string, string> { { "User Name", AuthUser.UserName }, { "CompID", CompID } };
                //Crashes.TrackError(exception, properties);

            }

        }

        private void SavePreferences()
        {
            if (RememberUser && !string.IsNullOrEmpty(AuthUser.UserName))
            {
                Preferences.Set("UsrName", AuthUser.UserName);
                Preferences.Set("UsrNameChk", RememberUser);
            }
            else
            {
                Preferences.Clear();
            }
        }




    }
}

