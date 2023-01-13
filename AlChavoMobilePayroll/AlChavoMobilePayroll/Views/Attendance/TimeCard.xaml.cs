using AlChavoMobilePayroll.ViewModels.Attendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlChavoMobilePayroll.Views.Attendance
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimeCard : ContentPage
    {
        public TimeCardViewModel ViewModel { get; set; }
        public TimeCard()
        {
            InitializeComponent();
            ViewModel = new TimeCardViewModel();
            this.BindingContext = ViewModel;

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.InitializeData();

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }
    }
}