using Microsoft.Toolkit.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordPressPCL.Models;
using System.Threading;
using WordPressUWP.Interfaces;
using GalaSoft.MvvmLight.Ioc;
using System.Diagnostics;
using WordPressUWP.Models;
using WordPressUWP.Helpers;

namespace WordPressUWP.Services
{
    public class PostsService : IIncrementalSource<Post>
    {
        private readonly IWordPressService _wordPressService;
        private readonly SettingsService _settings;

        public PostsService()
        {
            _wordPressService = SimpleIoc.Default.GetInstance<IWordPressService>();
            _settings = SimpleIoc.Default.GetInstance<SettingsService>();
        }

        public async Task<IEnumerable<Post>> GetPagedItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default(CancellationToken))
        {
            var posts = await _wordPressService.GetLatestPosts(pageIndex, pageSize);
            return PreparePosts(posts);
        }

        private IEnumerable<Post> PreparePosts(IEnumerable<Post> posts)
        {
            // Wrap the HTML content with headers, styles, scripts etc.
            int fontsize = _settings.GetSetting("fontsize", () => Config.DefaultFontSize, SettingLocality.Roamed);
            var settings = new HtmlSettings
            {
                FontSize = fontsize
            };
            foreach(var post in posts)
            {
                post.Content.Rendered = HtmlTools.WrapContent(post, settings);
            }
            return posts;
        }
    }
}
