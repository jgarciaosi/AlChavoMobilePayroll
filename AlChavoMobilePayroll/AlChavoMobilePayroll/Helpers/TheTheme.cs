using AlChavoMobilePayroll.DepServices;
using Xamarin.Forms;

namespace AlChavoMobilePayroll.Helpers
{
    public static class TheTheme
    {
        public static void SetTheme()
        {
            switch (Settings.Theme)
            {
                //default
                case 0:
                    //App.Current.UserAppTheme = OSAppTheme.Unspecified;
                    break;
                //light
                case 1:
                    //App.Current.UserAppTheme = OSAppTheme.Light;
                    break;
                //dark
                case 2:
                    //App.Current.UserAppTheme = OSAppTheme.Dark;
                    break;
            }

            //var nav = App.Current.MainPage as Xamarin.Forms.NavigationPage;

            //var e = DependencyService.Get<IPlatformTheme>();
            //if (App.Current.RequestedTheme == OSAppTheme.Dark)
            //{
            //    var background = (Color)App.Current.Resources["WindowBackgroundColorDark"];
            //    e?.SetStatusBarColor(background, false);
            //    if (nav != null)
            //    {
            //        nav.BarBackgroundColor = Color.Black;
            //        nav.BarTextColor = Color.White;
            //    }
            //}
            //else
            //{
            //    //var background = (Color)App.Current.Resources["WindowBackgroundColorLight"];
            //    //e?.SetStatusBarColor(background, true);
            //    if (nav != null)
            //    {
            //        nav.BarBackgroundColor = Color.White;
            //        nav.BarTextColor = Color.Black;
            //    }
            //}


        }
    }
}
