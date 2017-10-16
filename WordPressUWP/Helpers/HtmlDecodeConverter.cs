using System;
using System.Net;
using Windows.UI.Xaml.Data;

namespace WordPressUWP.Helpers
{
    public class HtmlDecodeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value is string)
            {
                return HtmlTools.Strip(WebUtility.HtmlDecode(value.ToString()));
            } else
            {
                return String.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is string)
            {
                return WebUtility.HtmlEncode(value.ToString());
            }
            else
            {
                return String.Empty;
            }
        }
    }
}
