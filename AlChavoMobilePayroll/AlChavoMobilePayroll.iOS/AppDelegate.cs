using Foundation;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter;
using Syncfusion.SfChart.XForms.iOS.Renderers;
using Syncfusion.XForms.iOS.Chat;
using UIKit;

namespace AlChavoMobilePayroll.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Rg.Plugins.Popup.Popup.Init();

            AppCenter.Start("d1caec54-39e8-472c-9656-e56af83ba0a7", typeof(Crashes));

            global::Xamarin.Forms.Forms.Init();
            SfChartRenderer.Init();
            SfChatRenderer.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
