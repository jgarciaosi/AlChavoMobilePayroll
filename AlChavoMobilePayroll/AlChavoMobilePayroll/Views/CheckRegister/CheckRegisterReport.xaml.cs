using AlChavoMobilePayroll.ViewModels.CheckRegister;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlChavoMobilePayroll.Views.CheckRegister
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckRegisterReport : ContentPage
    {
        public CheckRegisterReport()
        {
            InitializeComponent();
            BindingContext = new CheckRegisterReportViewModel();
        }
    }
}