using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Data;
using WordPressPCL.Models;

namespace WordPressUWP.Helpers
{
    public class FeaturedImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Embedded)
            {
                var p = (Embedded)value;
                var l = new List<MediaItem>(p.WpFeaturedmedia);
                return l.First().SourceUrl;
            }
            return String.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
