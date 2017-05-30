using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Common;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Template10.Utils;
using Windows.UI.Xaml.Navigation;
using WordPressPCL;
using WordPressPCL.Models;
using WordPressUWPApp.Utility;

namespace WordPressUWPApp.ViewModels
{
    public class DetailPageViewModel : ViewModelBase
    {
        public DetailPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                //Value = "Designtime value";
            }
            _client = new WordPressClient(ApiCredentials.WordPressUri);
            PostComments = new ObservableCollection<Comment>();
        }
        private WordPressClient _client;

        private Post _detailspost;
        public Post DetailsPost { get { return _detailspost; } set { Set(ref _detailspost, value); } }

        private string _contentWrapped;
        public string ContentWrapped { get { return _contentWrapped; } set { Set(ref _contentWrapped, value); } }


        private ObservableCollection<Comment> _postComments;
        public ObservableCollection<Comment> PostComments { get { return _postComments; } set { Set(ref _postComments, value); } }

        private async void GetPostComments()
        {
            var comments = await _client.GetCommentsForPost(DetailsPost.Id.ToString());
            PostComments.AddRange(comments);
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            DetailsPost = SessionState["selectedPost"] as Post;
            ContentWrapped = WrapContent(DetailsPost.Content.Rendered);
            GetPostComments();
            await Task.CompletedTask;
        }

        private string WrapContent(string rendered)
        {
            var prepend = $"<html><head><link rel=\"stylesheet\" href=\"ms-appx-web:///Assets/Web/Style.css\" type=\"text/css\" media=\"screen\" /></head><body><img src=\"{DetailsPost.Embedded.WpFeaturedmedia[0].MediaDetails.Sizes.MediumLarge.SourceUrl}\">";
            var append = "</body></html>";
            return prepend + rendered + append;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            if (suspending)
            {
                //suspensionState[nameof(Value)] = Value;
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }
    }
}

