using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using WordPressPCL.Models;

namespace WordPressUWP.Helpers
{
    public class FeaturedImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (parameter is IEnumerable<MediaItem>)
            {
                var l = new List<MediaItem>((IEnumerable<MediaItem>)parameter);
                return l.First().SourceUrl;
            }
            throw new ArgumentException("parameter must be an IEnumarable of MediaItem!");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
