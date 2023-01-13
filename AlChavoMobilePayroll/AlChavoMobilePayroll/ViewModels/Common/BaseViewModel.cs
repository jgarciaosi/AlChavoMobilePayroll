using AlChavoMobilePayments.Views.SManagement;
using AlChavoMobilePayroll.DepServices;
using AlChavoMobilePayroll.Models.SManagement.Permission;
using AlChavoMobilePayroll.Models.SManagement.SecurityManagement;
using AlChavoMobilePayroll.Services;

using AlChavoMobilePayroll.Views.SManagement;
using Microsoft.AppCenter.Crashes;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AlChavoMobilePayroll.ViewModels.Common
{
    public partial class BaseViewModel : INotifyPropertyChanged
    {

        #region"Services"
        public LoginService LoginService => DependencyService.Get<LoginService>();

        //public PermissionService PermissionService => DependencyService.Get<PermissionService>();
        public CompanyService CompanyService => DependencyService.Get<CompanyService>(); 
        public PunchService PunchService => DependencyService.Get<PunchService>();
        public UserService UserService => DependencyService.Get<UserService>();

        public PayeeService PayeeService => DependencyService.Get<PayeeService>();
        //public BillService BillService => DependencyService.Get<BillService>();
        //public OpenBillService OpenBillService => DependencyService.Get<OpenBillService>();
        //public SchedulePmtService SchedulePmtService => DependencyService.Get<SchedulePmtService>();
        public CheckRegisterService CheckRegisterService => DependencyService.Get<CheckRegisterService>();
        //public BillRegisterService BillRegisterService => DependencyService.Get<BillRegisterService>();
        public BankService BankService => DependencyService.Get<BankService>();
        //public TaxService TaxService => DependencyService.Get<TaxService>();
        //public ChartOfAcctService ChartOfAcctService => DependencyService.Get<ChartOfAcctService>();
        //public FileManagerService FileManagerService => DependencyService.Get<FileManagerService>();
        //     public AzureService AzureService => DependencyService.Get<AzureService>();
        public AttendanceService AttendanceService => DependencyService.Get<AttendanceService>();

        public EmployeeService EmployeeService => DependencyService.Get<EmployeeService>();

        public MailService MailService => DependencyService.Get<MailService>();

        #endregion

        #region "Properties"
        string title = string.Empty;

        private bool _isBusy;
        private bool _isRefreshing = false;

        public bool isBusy { get => _isBusy; set { _isBusy = value; ShowLoading(value); OnPropertyChanged(); } }// Permite identificar que se esta ejecutando una tarea y dar retroalimentacion a un usuario mediante un tipo load panel o progressbar
        public bool IsRefreshing { get { return _isRefreshing; } set { _isRefreshing = value; OnPropertyChanged(nameof(IsRefreshing)); } }// Permite identificar que se esta ejecutando una reload de un grid mediante el evento IsRefreshing
        public string Title { get { return title; } set { SetProperty(ref title, value); } } //Permite Settear Titulo desde View Model por herencia





        #endregion

        #region "Events"

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region "Methods"
        public void GetCrashError(Exception exception, IDictionary<string, string> properties = null, params ErrorAttachmentLog[] attachments)
        {
            Crashes.TrackError(exception, properties, attachments);
        }

        /// <summary>
        /// Despliega Popup de Notificacion
        /// </summary>
        /// <param name="Title">Titulo de Popup</param>
        /// <param name="Message">Mensaje de Popup</param>
        /// <param name="buttonText">Texto del Boton de Popup (Default = Accept )</param>
        /// <param name="NotificationType">Enum Success,Warnind, Alert</param>
        /// <param name="ReturnUrl">URL de Retorno en caso de Redireccion necesaria (Default = Empty ) </param>
        public async Task ShowNotificationPopup(string Title, string Message, string buttonText, NotificationType NotificationType, string ReturnUrl, bool ShowCloseButton)
        {
            await PopupNavigation.Instance.PushAsync(new NotificationPage(Title, Message, buttonText, NotificationType, ReturnUrl, ShowCloseButton));
        }
        public async Task CloseNotificationPopup()
        {
            await PopupNavigation.Instance.PopAsync();
        }

        /// <summary>
        /// Despliega Toast Notification
        /// </summary>
        /// <param name="message"></param>
        /// <param name="IsWarning"></param>


        public async void ShowACToastMessage(string message, bool IsWarning)
        {
            var options = new ToastOptions()
            {
                BackgroundColor = IsWarning ? Color.FromHex("#eb6a00") : Color.FromHex("#1673C3"),
                Duration = TimeSpan.FromSeconds(5),
                CornerRadius = 15,

                MessageOptions = new MessageOptions()
                {
                    Message = message,
                }
            };
            await Application.Current.MainPage.DisplayToastAsync(options);
        }



        /// <summary>
        /// Permite Utilizar por herencia la propiedad INotifyPropertyChanged
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
        private async void ShowLoading(bool value)
        {
            if (value)
                await PopupNavigation.Instance.PushAsync(new LoadingPage());
            else
            {
                if (PopupNavigation.Instance.PopupStack.Count > 0)
                    await PopupNavigation.Instance.PopAsync();
            }

        }
        public static void CallToPhone(String Phone)
        {
            try
            {
                PhoneDialer.Open(Phone);
            }
            catch (Exception)
            {

                Application.Current.MainPage.DisplayAlert("Alert", "Call Cannot be Performed, Try Later...", "Ok");
            }
        }
        public static async Task SendEmail(List<String> ToMail, List<String> CcMail, List<String> BccMail, String Subject, String Body)
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = String.IsNullOrEmpty(Subject) ? "AlChavo Mobile Payments" : Subject,
                    Body = String.IsNullOrEmpty(Body) ? "Message sended using AlChavo Mobile Payments" : Body,
                    To = ToMail,
                    Cc = CcMail,
                    Bcc = BccMail
                };

                await Email.ComposeAsync(message);

            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Mail cannot be Send, Try Later...", "Ok");
            }
        }
        public static async Task SendSMS(IEnumerable<String> Phones, string Message)
        {
            try
            {
                var message = new SmsMessage(Message, Phones);
                await Sms.ComposeAsync(message);
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "SMS cannot be Send, Try Later...", "Ok");
            }
        }

        //public async Task<MediaFile> GetImageFromFile()
        //{
        //    await CrossMedia.Current.Initialize();
        //    var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions { PhotoSize = PhotoSize.Full, SaveMetaData = true });
        //    return file;
        //}
        //public async Task<MediaFile> GetImageFromCamera()
        //{
        //    await CrossMedia.Current.Initialize();
        //    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
        //    {
        //        ShowACToastMessage("No Camera:( No camera avaialble.", true);
        //    }
        //    var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions { PhotoSize = PhotoSize.Medium, Directory = "AlChavoPayment" });
        //    return file;
        //}


        public Boolean IsNumeric(String input)
        {
            Decimal temp;
            Boolean result = Decimal.TryParse(input, out temp);
            return result;
        }

        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }


        public Boolean IsImage(string ParamExt)
        {
            List<string> ImageExtensions = new List<string> { ".JPG", ".JPEG", ".JPE", ".BMP", ".GIF", ".PNG" };

            if (ImageExtensions.Contains(ParamExt.ToUpperInvariant()))
                return true;
            else
                return false;
        }

      

        #endregion


        #region"Security Methods"
        public async Task<bool> SECSetUserPreferences(string CompID)
        {
            var userGUID = await SecureStorage.GetAsync("UserGUID");

            //=========================================================================
            // Si no Hay compañia en los preferences, busca la primera de su lista (Podría obtimizarse)
            if (string.IsNullOrEmpty(CompID))
            {
               // var CompanyList = await CompanyService.GetByUserIdMobileRestricted(userGUID).ConfigureAwait(false);
                //if (CompanyList != null)
                //{
                //    if (CompanyList.Count > 0)
                //    {
                //        CompID = CompanyList[0].CompID;
                //    }
                //    else
                //    {
                //        return false;
                //    }
                //}
                //else
                //{
                //    return false;
                //}

            }

            //=====Obtiene los permisos de la compañia seleccionada (Si no tiene alguno de los roles aprobados genera un logout)  
            //var PermissionList = await PermissionService.GetPermissions(CompID, userGUID).ConfigureAwait(true);
            //if (PermissionList == null || PermissionList.Count <= 0)
            //    return false;

            //====Obtiene los Dflts de la compañia actual=======================================================================
            //var CompanyDefaults = await CompanyService.GetAPDfltsByID(CompID).ConfigureAwait(true);
            //if (CompanyDefaults == null)
            //    return false;

            //====Settea la compañia y la establece como preferencia============================================================
            
           await SecureStorage.SetAsync("CompId", CompID);
            
            Preferences.Set("CompId", CompID);

            //App.SystemDflts.SecPermissionList = PermissionList;
            //App.SystemDflts.SecCompanyAPDefaults = CompanyDefaults;

            return true;

        }

        public bool SECAuthorizeAction(String PageName, Actions action)
        {
            //  bool IsAuthorized = SECValidatePermission(PageName, action);
            bool IsAuthorized = true;
            if (!IsAuthorized)
                ShowACToastMessage("User does not have permissions to execute the action: " + action.ToString(), true);

            return IsAuthorized;
        }
        //public bool SECValidatePermission(String PageName, Actions action)
        //{
        //    //List<PermissionResponse> PermissionList = App.SystemDflts.SecPermissionList;//Lista de Permisos
        //    //var cPermission = GetPermissionMapping(PageName, PermissionList);

        //    //if (cPermission != null)
        //    //    return GetPermissionValue(cPermission, action);
        //    //else
        //    //    return false;
        //}
        private PermissionResponse GetPermissionMapping(string pageName, List<PermissionResponse> permissionList)
        {
            switch (pageName)
            {
                //case nameof(CompanyDetail): { return permissionList.Find(x => x.PermissionID.Equals(5)); }

                //case nameof(UserIndex): { return permissionList.Find(x => x.PermissionID.Equals(396)); }
                //case nameof(UserDetail): { return permissionList.Find(x => x.PermissionID.Equals(396)); }

                //case nameof(PayeeIndex): { return permissionList.Find(x => x.PermissionID.Equals(185)); }
                //case nameof(PayeeDetail): { return permissionList.Find(x => x.PermissionID.Equals(303)); }

                //case nameof(BillDetail): { return permissionList.Find(x => x.PermissionID.Equals(307)); }

                //case nameof(OpenBillIndex): { return permissionList.Find(x => x.PermissionID.Equals(39)); }
                //case nameof(OpenBillDetail): { return permissionList.Find(x => x.PermissionID.Equals(39)); }

                //case nameof(ScheduleIndex): { return permissionList.Find(x => x.PermissionID.Equals(40)); }

                //case nameof(PaymentCheckIndex): { return permissionList.Find(x => x.PermissionID.Equals(152)); }

                //case nameof(CheckRegisterIndex): { return permissionList.Find(x => x.PermissionID.Equals(42)); }
                //case nameof(CheckRegisterDetail): { return permissionList.Find(x => x.PermissionID.Equals(42)); }

                //case nameof(BillRegisterIndex): { return permissionList.Find(x => x.PermissionID.Equals(191)); }
                //case nameof(BillRegisterPaymentsDetail): { return permissionList.Find(x => x.PermissionID.Equals(191)); }

                default: { return null; }
            }
        }
        private bool GetPermissionValue(PermissionResponse Permission, Actions ParamAction)
        {

            if (Permission == null)
                return false;
            else
                switch (ParamAction)
                {
                    case Actions.Add:
                        {
                            return Permission.AddYn;
                        }

                    case Actions.Approved:
                        {
                            return Permission.ApproveYn;
                        }

                    case Actions.BypassCashAccountNotAfilliated:
                        {
                            return Permission.BypassCashAccountNotAfilliated;
                        }

                    case Actions.BypassGLEdited:
                        {
                            return Permission.BypassGLEditedYn;
                        }

                    case Actions.BypassGLRestricted:
                        {
                            return Permission.BypassGLRestrictedYn;
                        }

                    case Actions.ChangePdFy:
                        {
                            return Permission.ChangePdFyYn;
                        }

                    case Actions.Copy:
                        {
                            return Permission.CopyYn;
                        }

                    case Actions.Delete:
                        {
                            return Permission.DeleteYn;
                        }

                    case Actions.Edit:
                        {
                            return Permission.EditYn;
                        }

                    case Actions.EditImage:
                        {
                            return Permission.EditImageYN;
                        }

                    case Actions.MoreActions:
                        {
                            return Permission.MoreActionsYn;
                        }

                    case Actions.Pay:
                        {
                            return Permission.PayYn;
                        }

                    case Actions.Post:
                        {
                            return Permission.PostYn;
                        }

                    case Actions.Print:
                        {
                            return Permission.PrintYn;
                        }

                    case Actions.Process:
                        {
                            return Permission.ProcessYn;
                        }

                    case Actions.Review:
                        {
                            return Permission.ReviewYn;
                        }

                    case Actions.Save:
                        {
                            return Permission.SaveYn;
                        }

                    case Actions.Submit:
                        {
                            return Permission.SubmitYn;
                        }

                    case Actions.Unpost:
                        {
                            return Permission.UnpostYN;
                        }

                    case Actions.View:
                        {
                            return Permission.ViewYn;
                        }

                    case Actions.Void:
                        {
                            return Permission.VoidYN;
                        }

                    default:
                        {
                            return false;
                        }
                }


        }

        #endregion




    }
}
