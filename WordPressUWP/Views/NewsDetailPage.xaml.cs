using System;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
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

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel.Item = e.Parameter as WordPressPCL.Models.Post;
            await ViewModel.Init();
        }

        private void CommentToggleButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            bool showCommentInput = CommentToggleButton.IsChecked ?? false;
            if (showCommentInput)
            {
                CommentInputSP.Height = double.NaN;
            }
            else
            {
                CommentInputSP.Height = 0;
            }
            
        }
    }
}
