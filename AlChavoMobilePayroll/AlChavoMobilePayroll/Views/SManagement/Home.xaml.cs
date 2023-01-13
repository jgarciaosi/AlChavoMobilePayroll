using AlChavoMobilePayroll.ViewModels.SManagement;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlChavoMobilePayroll.Views.SManagement
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        public HomeViewModel HomeViewModel { get; set; }

        public Home()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            HomeViewModel = new HomeViewModel();
            HomeViewModel.InitializeData();
           this.BindingContext = HomeViewModel;
        }
    }
}