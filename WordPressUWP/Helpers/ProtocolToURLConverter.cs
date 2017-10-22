using System;
using Windows.UI.Xaml.Data;

namespace WordPressUWP.Helpers
{
    public class ProtocolToURLConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var st = value.ToString();
            if (st.StartsWith("//www."))
            {
                st = "https:" + st;
            }
            return st;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
