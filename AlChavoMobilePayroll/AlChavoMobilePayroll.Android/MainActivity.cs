using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using System.Globalization;
using System.Threading;
using Plugin.Permissions;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Crashes;
using Plugin.Fingerprint;

namespace AlChavoMobilePayroll.Droid
{
    [Activity(Label = "AlChavoMobilePayroll", Icon = "@drawable/CompanyIcon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public App App { get; set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this);

            AppCenter.Start("ecc42b58-f548-4a37-b3df-0cbccbad6489", typeof(Crashes));
            CrossFingerprint.SetCurrentActivityResolver(() => Xamarin.Essentials.Platform.CurrentActivity);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            App = new App();
            LoadApplication(App);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            //Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed()
        {
            if (App.DoBack)
                base.OnBackPressed();

        }
        protected override void OnResume()
        {
            base.OnResume();
            // Here you would read it from where ever.
            var userSelectedCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = userSelectedCulture;
            SetCultureToUSEnglish();


        }

        private void SetCultureToUSEnglish()
        {
            CultureInfo englishUSCulture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = englishUSCulture;
        }
    }
}