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
    public partial class TimeOff : ContentPage
    {
        public TimeOffViewModel timeOffViewModel { get; set; }

        public TimeOff()
        {
            InitializeComponent();
            timeOffViewModel= new TimeOffViewModel();
            this.BindingContext = timeOffViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();            
            await timeOffViewModel.InitializeData();

        }


    }
}