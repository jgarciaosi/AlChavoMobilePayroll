using AlChavoMobilePayroll.ViewModels.DownloadDocuments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlChavoMobilePayroll.Views.DownloadDocuments
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DocumentsIndex : ContentPage
    {

        public DocumentsIndexViewModel ViewModel { get; set; }
        public DocumentsIndex()
        {
            InitializeComponent();
            ViewModel = new DocumentsIndexViewModel();
            this.BindingContext = ViewModel;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.LoadDocuments();

        }


    }
}