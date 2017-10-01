using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using WordPressPCL.Models;

namespace WordPressUWP.Helpers
{
    public class HtmlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Post)
            {
                var post = (Post)value;
                return HtmlTools.WrapContent(post.Content.Rendered);
            }
            return String.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
