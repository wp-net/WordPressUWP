using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Diagnostics;
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.Init(WindowStates.CurrentState);
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

        private void ReplyButton_Click(object sender, RoutedEventArgs e)
        {
            if(sender is HyperlinkButton button){
                if (button.Tag is WordPressPCL.Models.CommentThreaded comment)
                {
                    Debug.WriteLine(comment.Id);
                    ViewModel.CommentReply = comment;
                    ViewModel.Reply();
                }
            }
        }

        private void RoundImageEx_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            var img = (RoundImageEx)sender;
            Debug.WriteLine($"imageex error: {e.ErrorMessage} | {img.Source}");
        }
    }
}
