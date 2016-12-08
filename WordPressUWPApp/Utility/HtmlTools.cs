using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WordPressUWPApp.Utility
{
    public static class HtmlTools
    {
        public static string Strip(string text)
        {
            return Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
        }


        public static string GetContent(DependencyObject obj)
        {
            return (string)obj.GetValue(ContentProperty);
        }


        public static void SetContent(DependencyObject obj, string value)
        {
            obj.SetValue(ContentProperty, value);
        }

        // Using a DependencyProperty as the backing store for HTML.  This enables animation, styling, binding, etc...  
        public static readonly DependencyProperty ContentProperty = DependencyProperty.RegisterAttached(
              "Content", 
              typeof(string), 
              typeof(HtmlTools), 
              new PropertyMetadata(string.Empty, OnContentChanged));


        //private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    WebView wv = d as WebView;
        //    if (wv != null)
        //    {
        //        wv.NavigateToString((string)e.NewValue);
        //    }
        //}
        private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WebView wv = d as WebView;
            var content = e.NewValue as string;
            if (string.IsNullOrEmpty(content))
            {
                return;
            }
            wv?.NavigateToString(content);
        }
    }
}
