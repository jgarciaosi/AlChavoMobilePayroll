using AlChavoMobilePayroll.ViewModels.User;
using Xamarin.Forms;

namespace AlChavoMobilePayroll.Views.User
{

    public partial class UserIndex : ContentPage
    {
        UserIndexViewModel ViewModel { get; set; }

        public UserIndex()
        {
            InitializeComponent();
            BindingContext = new UserIndexViewModel();

        }
    }
}