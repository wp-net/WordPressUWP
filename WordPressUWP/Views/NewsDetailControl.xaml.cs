using System;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using WordPressUWP.Models;

namespace WordPressUWP.Views
{
    public sealed partial class NewsDetailControl : UserControl
    {
        public SampleOrder MasterMenuItem
        {
            get { return GetValue(MasterMenuItemProperty) as SampleOrder; }
            set { SetValue(MasterMenuItemProperty, value); }
        }

        public static readonly DependencyProperty MasterMenuItemProperty = DependencyProperty.Register("MasterMenuItem", typeof(SampleOrder), typeof(NewsDetailControl), new PropertyMetadata(null));

        public NewsDetailControl()
        {
            InitializeComponent();
        }
    }
}
