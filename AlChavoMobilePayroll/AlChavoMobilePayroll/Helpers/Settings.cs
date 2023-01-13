using AlChavoMobilePayroll.Models.System;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace AlChavoMobilePayroll.Helpers
{
    public static class Settings
    {
        // 0 = default, 1 = light, 2 = dark
        const int theme = 0;
        public static int Theme
        {
            get => Preferences.Get(nameof(Theme), theme);
            set => Preferences.Set(nameof(Theme), value);
        }

        //public static Theme ThemeOption
        //{
        //    get => (Theme)Preferences.Get(nameof(ThemeOption), HasDefaultThemeOption ? (int)Theme.Default : (int)Theme.Light);
        //    set => Preferences.Set(nameof(ThemeOption), (int)value);
        //}

        //public static bool HasDefaultThemeOption
        //{
        //    get
        //    {
        //        var minDefaultVersion = new Version(13, 0);
        //        if (DeviceInfo.Platform == DevicePlatform.UWP)
        //            minDefaultVersion = new Version(10, 0, 17763, 1);
        //        else if (DeviceInfo.Platform == DevicePlatform.Android)
        //            minDefaultVersion = new Version(10, 0);

        //        return DeviceInfo.Version >= minDefaultVersion;
        //    }
        //}


    }
}
