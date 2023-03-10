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
    public partial class Schedule : ContentPage
    {
        public ScheduleViewModel ViewModel { get; set; }
            public Schedule()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            ViewModel = new ScheduleViewModel();
            ViewModel.InitializeData();
            this.BindingContext = ViewModel;

        }
    }
}