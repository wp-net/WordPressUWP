using System;
using Windows.UI.Core;
using Windows.UI.Xaml;
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
            Window.Current.SizeChanged += Current_SizeChanged;
        }

        private void Current_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            if(Window.Current.Bounds.Width >= 1300)
            {
                CommentsColumn.Margin = new Thickness(0, 0, 0, 0);
                CommentToggleButton.IsChecked = false;
            }
        }

        private void CommentToggleButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            bool showComments = CommentToggleButton.IsChecked ?? false;
            if (showComments)
            {
                CommentsColumn.Margin = new Thickness(-400, 0, 0, 48);
            } else
            {
                CommentsColumn.Margin = new Thickness(0, 0, -400, 0);
            }
        }
    }
}
