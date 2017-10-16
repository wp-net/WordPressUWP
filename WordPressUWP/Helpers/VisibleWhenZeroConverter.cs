using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace WordPressUWP.Helpers
{
    public class VisibleWhenZeroConverter : IValueConverter
    {
        public object Convert(object v, Type t, object p, string l) =>
            Equals(0, (int)v) ? Visibility.Visible : Visibility.Collapsed;

        public object ConvertBack(object v, Type t, object p, string l) => null;
    }
}
