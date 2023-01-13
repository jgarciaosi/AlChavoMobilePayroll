using System;
using System.Globalization;
using Xamarin.Forms;

namespace AlChavoMobilePayroll.ExtensionMethods
{
     class PrintChecksBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            int pMethod = (int)value;

            if (pMethod.Equals(5))
                return true;


            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
            //throw new NotImplementedException();
        }
    }
}