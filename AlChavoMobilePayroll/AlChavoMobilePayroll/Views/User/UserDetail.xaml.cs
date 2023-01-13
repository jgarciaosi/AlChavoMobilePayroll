
using AlChavoMobilePayroll.ViewModels.User;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlChavoMobilePayroll.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserDetail : ContentPage
    {
        public UserDetail()
        {
            InitializeComponent();
            BindingContext = new UserDetailViewModel();
        }
    }
}