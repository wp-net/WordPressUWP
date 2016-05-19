using Template10.Mvvm;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using WordPressUWP.Models;
using WordPressUWP;
using WordPressUWPApp.Utility;
using Template10.Utils;
using WordPressUWPApp.Models;

namespace WordPressUWPApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public ObservableCollection<PostWithMedia> Posts { get; } = new ObservableCollection<PostWithMedia>();
        private WordPressClient _client;

        public MainPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Value = "Designtime value";
            }

            Initialize();
        }

        public async void Initialize()
        {

            _client = new WordPressClient(ApiCredentials.WordPressUri);
            _client.Username = ApiCredentials.Username;
            _client.Password = ApiCredentials.Password;
            var posts = await _client.ListPosts();
            foreach (var post in posts)
            {
                PostWithMedia newpost = new PostWithMedia() { Post = post };
                if(post.FeaturedMedia != 0)
                {
                    newpost.FeaturedMediaFull = await _client.GetMedia(post.FeaturedMedia.ToString());
                }
                // Clean excerpt
                if (newpost?.Post?.Excerpt?.Rendered != null)
                {
                    newpost.Post.Excerpt.Raw = HtmlTools.Strip(newpost.Post.Excerpt.Rendered);
                }
                Posts.Add(newpost);
            }
        }

        string _Value = "Gas";
        public string Value { get { return _Value; } set { Set(ref _Value, value); } }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            if (suspensionState.Any())
            {
                Value = suspensionState[nameof(Value)]?.ToString();
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            if (suspending)
            {
                suspensionState[nameof(Value)] = Value;
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        public void GotoDetailsPage() =>
            NavigationService.Navigate(typeof(Views.DetailPage), Value);

        public void GotoSettings() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 0);

        public void GotoPrivacy() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 1);

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 2);

    }
}

