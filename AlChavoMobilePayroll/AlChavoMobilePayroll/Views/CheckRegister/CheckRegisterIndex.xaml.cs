using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlChavoMobilePayroll.ViewModels.CheckRegister;
using Microcharts;
using SkiaSharp;
using Syncfusion.SfCalendar.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlChavoMobilePayroll.Views.CheckRegister
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckRegisterIndex : ContentPage
    {
        public CheckRegisterIndexViewModel ViewModel { get; set; }

        public CheckRegisterIndex()
        {
            InitializeComponent();
            ViewModel = new CheckRegisterIndexViewModel();
            this.BindingContext = ViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.InitializeData();
        }

    }
}