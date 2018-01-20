using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Data;
using WordPressPCL.Models;

namespace WordPressUWP.Helpers
{
    public class AuthorNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value is List<User> authors)
            {
                return authors[0].Name;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
