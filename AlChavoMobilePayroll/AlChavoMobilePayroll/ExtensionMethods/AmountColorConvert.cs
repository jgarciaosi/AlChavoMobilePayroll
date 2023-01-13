using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace AlChavoMobilePayroll.ExtensionMethods
{
    class AmountColorConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value != null)
            {
                float s = (float)value;

                if (s < 0)
                {
                    return Color.Firebrick;
                }
                //else
                //{
                //    return Color.FromHex("#051c3d");
                //}
            }
            return Color.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
