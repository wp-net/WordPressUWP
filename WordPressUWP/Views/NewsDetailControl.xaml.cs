using System;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WordPressPCL.Models;

namespace WordPressUWP.Views
{
    public sealed partial class NewsDetailControl : UserControl
    {
        public Post MasterMenuItem
        {
            get { return GetValue(MasterMenuItemProperty) as Post; }
            set { SetValue(MasterMenuItemProperty, value); }
        }

        public static readonly DependencyProperty MasterMenuItemProperty = DependencyProperty.Register("MasterMenuItem", typeof(Post), typeof(NewsDetailControl), new PropertyMetadata(null));

        public NewsDetailControl()
        {
            InitializeComponent();
        }
    }
}
