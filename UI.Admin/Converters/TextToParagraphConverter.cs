using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace UI.Admin.Converters
{
    [ValueConversion(typeof(string), typeof(string))]
    public class TextToParagraphConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int length = 75;
            if (parameter != null)
            {
                try
                {
                    length = int.Parse(parameter as string);
                }
                catch (Exception)
                {
                }
            }

            string text = value as string;
            if (text == null)
                return null;

            if (text.Length <= length)
                return text;

            return string.Format("{0}...", text.Substring(0, length));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
