using AlChavoMobilePayroll.ViewModels.CheckRegister;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlChavoMobilePayroll.Views.CheckRegister
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckRegisterDetail : ContentPage
    {

        //public CheckRegisterDetailViewModel CheckRegisterDetailViewModel { get; set; }

        //public CheckRegisterDetail()
        //{
        //    InitializeComponent();

        //}

        //protected override async void OnAppearing()
        //{
        //    base.OnAppearing();
        //    CheckRegisterDetailViewModel = new CheckRegisterDetailViewModel();
        //    CheckRegisterDetailViewModel.InitializeData();
        //    this.BindingContext = CheckRegisterDetailViewModel;

        //}

        public CheckRegisterDetailViewModel ViewModel { get; set; }
        public CheckRegisterDetail()
        {
            ViewModel = new CheckRegisterDetailViewModel();
            this.BindingContext = ViewModel;
            InitializeComponent();
        }

    }







}