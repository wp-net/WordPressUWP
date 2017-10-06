using System.Text;
using System.Text.RegularExpressions;
using Windows.UI.Xaml;
using WordPressUWP.Services;

namespace WordPressUWP.Helpers
{

    public static class HtmlTools

    {
        public static string Strip(string text)
        {
            return Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
        }

        public static string WrapContent(string rendered)
        {
            var sb = new StringBuilder();
            var isDark = ThemeSelectorService.Theme == ElementTheme.Dark;

            sb.Append("<html><head>");
            sb.Append("<link rel=\"stylesheet\" href=\"ms-appx-web:///Assets/Web/Style.css\" type=\"text/css\" media=\"screen\" />");
            if (isDark)
            {
                sb.Append("<link rel=\"stylesheet\" href=\"ms-appx-web:///Assets/Web/Dark.css\" type=\"text/css\" media=\"screen\" />");
            }
            else
            {
                sb.Append("<link rel=\"stylesheet\" href=\"ms-appx-web:///Assets/Web/Light.css\" type=\"text/css\" media=\"screen\" />");
            }

            sb.Append(rendered);
            sb.Append("</head><body>");

            return sb.ToString();
        }
    }
}
