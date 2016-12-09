using Template10.Mvvm;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using WordPressPCL.Models;
using WordPressPCL;
using WordPressUWPApp.Utility;
using Template10.Utils;
using Windows.UI.Popups;

namespace WordPressUWPApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private ObservableCollection<Post> _posts;
        public ObservableCollection<Post> Posts { get { return _posts; } set { Set(ref _posts, value); } }
        private WordPressClient _client;

        public MainPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {

            }

            Initialize();
        }

        public async void Initialize()
        {

            _client = new WordPressClient(ApiCredentials.WordPressUri);
            var posts = await _client.ListPosts(true);
            foreach (var post in posts)
            {
                // Clean excerpt
                if (post?.Excerpt?.Rendered != null)
                {
                    post.Excerpt.Raw = HtmlTools.Strip(post.Excerpt.Rendered);
                }
            }
            Posts = new ObservableCollection<Post>(posts);
        }

        private Post _selectedPost;
        public Post SelectedPost { get { return _selectedPost; } set { Set(ref _selectedPost, value); } }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            if (suspensionState.Any())
            {
                // save to suspensionState
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            if (suspending)
            {
                suspensionState["selectedPost"] = SelectedPost;
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        public async void ClickCommand(object sender, object parameter)
        {
            var arg = parameter as Windows.UI.Xaml.Controls.ItemClickEventArgs;
            SelectedPost = arg.ClickedItem as Post;
            if (SessionState.ContainsKey("selectedPost"))
            {
                SessionState.Remove("selectedPost");
            }
            SessionState.Add("selectedPost", SelectedPost);
            NavigationService.Navigate(typeof(Views.DetailPage));
        }

        public void GotoSettings() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 0);

        public void GotoPrivacy() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 1);

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 2);

    }
}

