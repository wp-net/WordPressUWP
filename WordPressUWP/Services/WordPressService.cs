using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using WordPressPCL;
using WordPressPCL.Models;
using WordPressPCL.Utility;
using WordPressUWP.Helpers;
using WordPressUWP.Interfaces;
using System;

namespace WordPressUWP.Services
{
    public class WordPressService : ViewModelBase, IWordPressService
    {
        private WordPressClient _client;
        private ApplicationDataContainer _localSettings;
        private User _currentUser;

        private bool _isAuthenticated;
        public bool IsAuthenticated
        {
            get { return _isAuthenticated; }
            set { Set(ref _isAuthenticated, value); }
        }

        public User CurrentUser
        {
            get { return _currentUser; }
            set { Set(ref _currentUser, value); }
        }

        public WordPressService()
        {
            _client = new WordPressClient(ApiCredentials.WordPressUri);
            _localSettings = ApplicationData.Current.LocalSettings;
            Init();
        }

        public async void Init()
        {

            IsAuthenticated = false;
            var username = _localSettings.ReadString("Username");
            if (username != null)
            {
                // get password
                var jwt = SettingsStorageExtensions.GetCredentialFromLocker(username);
                if (jwt != null && !string.IsNullOrEmpty(jwt.Password))
                {
                    // set jwt
                    _client.SetJWToken(jwt.Password);
                    IsAuthenticated = await _client.IsValidJWToken();
                    if (IsAuthenticated)
                    {
                        CurrentUser = await _client.Users.GetCurrentUser();
                    }
                }
            }
        }

        public async Task<bool> AuthenticateUser(string username, string password)
        {
            _client.AuthMethod = AuthMethod.JWT;
            await _client.RequestJWToken(username, password);
            var isAuthenticated = await IsUserAuthenticated();

            if (isAuthenticated)
            {
                // Store username & JWT token for logging in on next app launch
                SettingsStorageExtensions.SaveString(_localSettings, "Username", username);
                SettingsStorageExtensions.SaveCredentialsToLocker(username, _client.GetToken());
                CurrentUser = await _client.Users.GetCurrentUser();
            }

            return isAuthenticated;
        }

        public async Task<List<CommentThreaded>> GetCommentsForPost(int postid)
        {
            var comments = await _client.Comments.GetAllCommentsForPost(postid);
            return comments.ToThreaded();
        }

        public async Task<IEnumerable<Post>> GetLatestPosts(int page = 0, int perPage = 20)
        {
            Debug.WriteLine($"loading page {page}");
            page++;

            var posts = await _client.Posts.Query(new PostsQueryBuilder()
            {
                Page = page,
                PerPage = perPage,
                Embed = true
            });

            return posts;
        }

        public User GetUser()
        {
            return CurrentUser;
        }

        public async Task<bool> IsUserAuthenticated()
        {
            IsAuthenticated = await _client.IsValidJWToken();
            return IsAuthenticated;
        }

        public async Task<Comment> PostComment(int postId, string text, int replyto = 0)
        {
            var comment = new Comment(postId, text);
            if (replyto != 0)
            {
                comment.ParentId = replyto;
            }
            return await _client.Comments.Create(comment);
        }

        public async Task<bool> Logout()
        {
            _client.Logout();
            IsAuthenticated = await _client.IsValidJWToken();
            SettingsStorageExtensions.RemoveCredentialsFromLocker();
            return IsAuthenticated;
        }
    }
}
