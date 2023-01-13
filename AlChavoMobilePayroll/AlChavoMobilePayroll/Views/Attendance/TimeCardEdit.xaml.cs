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
    public partial class TimeCardEdit : ContentPage
    {
        public TimeCardEdit()
        {
            InitializeComponent();
            this.BindingContext= new TimeCardEditViewModel();
        }

       
        public TimeCardEditViewModel ViewModel { get; set; }
      
     
        private void ImageButton_Clicked(object sender, EventArgs e)
        {

        }

        private void dateTimePicker_SelectionChanged(object sender, EventArgs e)
        {

        }
    }
}