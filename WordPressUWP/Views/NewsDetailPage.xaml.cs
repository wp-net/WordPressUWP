using System.Diagnostics;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WordPressUWP.Helpers;
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
            ViewModel.SelectedPost = e.Parameter as WordPressPCL.Models.Post;
            await ViewModel.Init();
        }

        private void CommentToggleButton_Click(object sender, RoutedEventArgs e)
        {
            bool showCommentInput = CommentToggleButton.IsChecked ?? false;
            ToggleCommentInput(showCommentInput);
        }

        private void ToggleCommentInput(bool show)
        {
            CommentToggleButton.IsChecked = show;
            if (show)
            {
                CommentInputGrid.Height = double.NaN;
            }
            else
            {
                CommentInputGrid.Height = 0;
            }
        }

        private void ReplyButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (sender is HyperlinkButton button)
            {
                if (button.Tag is WordPressPCL.Models.CommentThreaded comment)
                {
                    ViewModel.CommentReply = comment;
                    ToggleCommentInput(true);
                    CommentInput.Focus(FocusState.Pointer);
                }
            }
        }

        private void NewsDetailControl_Swiped(object sender, SwipedEventArgs e)
        {
            Debug.WriteLine(e.Direction);
            if (e.Direction.Equals(SwipeDirection.Left))
                PostPivot.SelectedIndex = 1;
            else if (e.Direction.Equals(SwipeDirection.Right))
                ViewModel.NavigationService.GoBack();
        }

        private void FirstCommentBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CommentListView.Items.Count > 0)
                CommentListView.ScrollIntoView(CommentListView.Items[0]);
        }

        private void LastCommentBtn_Click(object sender, RoutedEventArgs e)
        {
            var itemIndex = CommentListView.Items.Count - 1;
            if (itemIndex > 0)
                CommentListView.ScrollIntoView(CommentListView.Items[itemIndex]);
        }
    }
}
