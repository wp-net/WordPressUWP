using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using WordPressPCL.Models;

namespace WordPressUWP.Helpers
{
    class EmbeddedRepliesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is IEnumerable<IList<Comment>>)
            {
                var v = (IEnumerable<IList<Comment>>)value;
                return new List<Comment>(v.First());
            }
            else return new List<Comment>();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
