using AlChavoMobilePayroll.ViewModels.SManagement;
using System;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlChavoMobilePayroll.Views.SManagement
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Punch : ContentPage
    {

        public PunchViewModel viewModel { get; set; }
        public Punch()
        {
            InitializeComponent();
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            viewModel = new PunchViewModel();
            await viewModel.InitializeData();
            this.BindingContext = viewModel;
        }

        private void PunchClicked(object sender, EventArgs args)
        {  
            var button = (Button)sender;
            viewModel.PunchInOrOut = button.ClassId;
        }

        private void MediaCaptured(object sender, MediaCapturedEventArgs e)
        {
            viewModel.CanPhoto = e.ImageData;
            cameraView.CaptureMode = CameraCaptureMode.Photo;
        }
         
    }
}