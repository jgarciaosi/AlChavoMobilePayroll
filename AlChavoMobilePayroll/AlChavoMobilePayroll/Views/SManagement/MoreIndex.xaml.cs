//using AlChavoMobilePayroll.ViewModels.CheckRegister;
using AlChavoMobilePayroll.ViewModels.SManagement;
//using AlChavoMobilePayroll.Views.Bill;
//using AlChavoMobilePayroll.Views.CheckRegister;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlChavoMobilePayroll.Views.SManagement
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoreIndex : ContentPage
    {
       public MoreIndex()
        {
            InitializeComponent();
            this.BindingContext = new MoreIndexViewModel();
        }



    }
}