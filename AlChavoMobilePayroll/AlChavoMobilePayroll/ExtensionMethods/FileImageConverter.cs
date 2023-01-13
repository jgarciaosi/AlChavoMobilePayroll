using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace AlChavoMobilePayroll.ExtensionMethods
{
    class FileImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var FileName = (string)value;

            return GetImageByExtension(FileName);


        }

        private ImageSource GetImageByExtension(string fileName)
        {
            var Ext = Path.GetExtension(fileName);

            if (Ext.ToLower().Equals(".pdf"))
                return ConvertToImageSource("pdf_icon");
         
            if (Ext.ToLower().Equals(".txt"))
                return ConvertToImageSource("txt_icon");
            
            if (Ext.ToLower().Equals(".doc") || Ext.ToLower().Equals(".docx"))
                return ConvertToImageSource("docx_icon");
            
            if (Ext.ToLower().Equals(".xls") || Ext.ToLower().Equals(".xlsx"))
                return ConvertToImageSource("xlsx_icon");


            return ConvertToImageSource("file_icon");

        }

        private ImageSource ConvertToImageSource(string fileName)
        {
         
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                 return   ImageSource.FromFile($"Images/{fileName}");
                   
                case Device.Android:
                   var source=ImageSource.FromFile(fileName);
                    return source;


                case Device.UWP:
                    return ImageSource.FromFile($"Images/{fileName}");
                                   
            }
            return ImageSource.FromFile("");
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }





    }
}
