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
    public partial class TimeOffReport : ContentPage
    {
        public TimeOffViewReportModel timeOffViewModel { get; set; }

        public TimeOffReport()
        {
            InitializeComponent();

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            timeOffViewModel = new TimeOffViewReportModel();
            timeOffViewModel.InitializeData();
            this.BindingContext = timeOffViewModel;



        }
    }
}