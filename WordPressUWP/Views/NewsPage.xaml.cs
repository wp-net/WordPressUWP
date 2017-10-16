using System;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using WordPressUWP.ViewModels;

namespace WordPressUWP.Views
{
    public sealed partial class NewsPage : Page
    {
        private NewsViewModel ViewModel
        {
            get { return DataContext as NewsViewModel; }
        }

        public NewsPage()
        {
            InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.Init(WindowStates.CurrentState);
        }
    }
}
