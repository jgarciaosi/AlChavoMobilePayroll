using AlChavoMobilePayroll.ViewModels.Common;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;
using static AlChavoMobilePayroll.ViewModels.Common.BaseViewModel;

namespace AlChavoMobilePayments.Views.SManagement
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationPage : PopupPage
    {
        public NotificationPage(string Title, string Message, string buttonText, NotificationType NotificationType, string ReturnUrl, bool ShowCloseButton)
        {
            InitializeComponent();

            BindingContext = new NotificationViewModel(Title, Message, buttonText, NotificationType, ReturnUrl, ShowCloseButton);

        }
    }
}