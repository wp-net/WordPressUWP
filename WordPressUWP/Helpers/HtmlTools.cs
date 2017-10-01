using System.Text.RegularExpressions;

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
            var prepend = $"<html><head><link rel=\"stylesheet\" href=\"ms-appx-web:///Assets/Web/Style.css\" type=\"text/css\" media=\"screen\" /></head><body>";
            var append = "</body></html>";
            return prepend + rendered + append;
        }
    }
}
