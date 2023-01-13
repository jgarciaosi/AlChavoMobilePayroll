using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AlChavoMobilePayroll.ViewModels.Common
{
    public class NotificationViewModel : Common.BaseViewModel
    {


        public String PopupTittle { get; set; }
        public String PopupDescription { get; set; }
        public String PopupButtonText { get; set; }
        public String PopupReturnUrl { get; set; }
        public bool PopupIsSuccess { get; set; }
        public bool PopupIsError { get; set; }
        public bool PopupIsAlert { get; set; }
        public bool ShowCloseBnt { get; set; }



        public ICommand GotoCommand { set; get; }
        public ICommand CloseCommand { set; get; }



        public NotificationViewModel(string title, string message, string buttonText, NotificationType notificationType, string returnUrl,bool ShowCloseButton)
        {
            GotoCommand = new Command(() => RedirectToPageCommand());
            CloseCommand = new Command(() => CloseNotification());



            PopupTittle = title;
            PopupDescription = message;
            PopupButtonText = String.IsNullOrEmpty(buttonText) ? "Accept" : buttonText;

            PopupIsSuccess = notificationType.Equals(NotificationType.Success) ? true : false;
            PopupIsError = notificationType.Equals(NotificationType.Error) ? true : false;
            PopupIsAlert = notificationType.Equals(NotificationType.Alert) ? true : false;
            ShowCloseBnt = ShowCloseButton;
            PopupReturnUrl = returnUrl;
        }

        public async void RedirectToPageCommand()
        {

            if (!String.IsNullOrEmpty(PopupReturnUrl))
            {
                if (PopupReturnUrl.Contains("www"))
                {
                    try
                    {
                        await Browser.OpenAsync(new Uri(PopupReturnUrl), BrowserLaunchMode.SystemPreferred);
                    }
                    catch (Exception ex)
                    {
                        Crashes.TrackError(ex);
                    }
                }
                else
                {
                    CloseNotificationPopup();
                    await Shell.Current.GoToAsync($"//{PopupReturnUrl}");
                }
            }
            else
            {
                CloseNotificationPopup();
            }

        }

        public async void CloseNotification()
        {
            await CloseNotificationPopup();
        }


    }
}
