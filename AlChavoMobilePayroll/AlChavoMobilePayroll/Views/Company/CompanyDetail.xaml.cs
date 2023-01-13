using AlChavoMobilePayroll.ViewModels.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlChavoMobilePayroll.Views.Company
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompanyDetail : ContentPage
    {
        public CompanyDetail()
        {
            InitializeComponent();
            BindingContext = new CompanyDetailViewModel();
        }
    }
}