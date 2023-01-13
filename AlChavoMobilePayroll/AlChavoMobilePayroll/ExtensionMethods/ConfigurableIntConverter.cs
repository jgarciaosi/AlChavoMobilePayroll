using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace AlChavoMobilePayroll.ExtensionMethods
{
    class ConfigurableIntConverter<T> : IValueConverter
    {
        public ConfigurableIntConverter() { }

        public ConfigurableIntConverter(T trueResult, T falseResult)
        {
            TrueResult = trueResult;
            FalseResult = falseResult;
        }

        public T TrueResult { get; set; }
        public T FalseResult { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int s = (int)value;
          
            if (TrueResult == null || FalseResult == null)
                return FalseResult;

            return s.Equals(1) ? TrueResult : FalseResult;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int s = (int)value;
            if (TrueResult == null || FalseResult == null)
                return FalseResult;

            return s.Equals(1) ? TrueResult : FalseResult;
        }

    }
}
