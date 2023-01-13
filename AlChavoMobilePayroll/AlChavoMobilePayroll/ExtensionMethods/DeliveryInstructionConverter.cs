using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace AlChavoMobilePayroll.ExtensionMethods
{
    class DeliveryInstructionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int s = (int)value;

            switch (s)
            {
                case 1:
                    return "Electronic";
                case 2:
                    return "On Hand";
                case 3:
                    return "Send by Mail";
                case 4:
                    return "OSI Staff";
                case 5:
                    return "On Site";
                default:
                    return "N/A";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
