using System;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using WordPressUWP.Models;
using WordPressUWP.ViewModels;

namespace WordPressUWP.Views
{
    public sealed partial class NewsDetailPage : Page
    {
        private NewsDetailViewModel ViewModel
        {
            get { return DataContext as NewsDetailViewModel; }
        }

        public NewsDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel.Item = e.Parameter as SampleOrder;
        }
    }
}
