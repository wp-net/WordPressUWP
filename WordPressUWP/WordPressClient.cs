using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;
using WordPressUWP.Models;

namespace WordPressUWP
{
	public class WordPressClient
	{
		private readonly string _wordPressUri;

		public WordPressClient(string uri)
		{
			if (string.IsNullOrWhiteSpace(uri))
			{
				throw new ArgumentNullException(nameof(uri));
			}

			if (!uri.EndsWith("/"))
			{
				uri += "/";
			}

			_wordPressUri = uri;
		}

		public string WordPressUri
		{
			get { return _wordPressUri; }
		}

        public String Username { get; set; }
        public String Password { get; set; }


        #region Post methods 
        public async Task<IList<Post>> ListPosts()
		{
            // default values 
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return await GetRequest<Post[]>($"posts").ConfigureAwait(false);
		}

		public async Task<Post> GetPost(String id)
		{
			return await GetRequest<Post>($"posts/{id}").ConfigureAwait(false);
		}

        public async Task<Post> CreatePost(Post postObject)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(postObject).ToString(), Encoding.UTF8, "application/json");
            return await PostRequest<Post>($"posts", postBody);
        }
        #endregion

        #region Comment methods
        public async Task<IList<Comment>> ListComments()
		{
			return await GetRequest<Comment[]>("comments").ConfigureAwait(false);
		}

        public async Task<IList<Comment>> GetCommentsForPost(string id)
        {
            return await GetRequest<Comment[]>($"comments?post={id}");
        }

		public async Task<Comment> GetComment(string id)
		{
			return await GetRequest<Comment>($"comments/{id}").ConfigureAwait(false);
		}
        #endregion

        #region Tag methods
        public async Task<Tag> CreateTag(Tag tagObject)
        { 
             var postBody = new StringContent(JsonConvert.SerializeObject(tagObject).ToString(), Encoding.UTF8, "application/json"); 
             return await PostRequest<Tag>($"tags", postBody); 
        }
        #endregion

        #region User methods
        public async Task<User> GetCurrentUser()
        {
            return await GetRequest<User>($"users/me", true).ConfigureAwait(false);
        }
        #endregion

        #region Media methods
        public async Task<Media> GetMedia(string id)
        {
            return await GetRequest<Media>($"media/{id}").ConfigureAwait(false);
        }
        #endregion

        #region Authorize methods
        public async void DoOAuth()
        {
            string startURL = "http://api.medienstudio.net/oauth1/request?oauth_consumer_key=Oa8UDlP8ToPh";

            // "request": "http://api.medienstudio.net/oauth1/request",
            // "authorize": "http://api.medienstudio.net/oauth1/authorize",
            // "access": "http://api.medienstudio.net/oauth1/access",
            string endURL = "http://api.medienstudio.net";

            System.Uri startURI = new System.Uri(startURL);
            System.Uri endURI = new System.Uri(endURL);

            string result;

            try
            {
                var webAuthenticationResult =
                    await WebAuthenticationBroker.AuthenticateAsync(
                    WebAuthenticationOptions.None,
                    startURI,
                    endURI);

                switch (webAuthenticationResult.ResponseStatus)
                {
                    case WebAuthenticationStatus.Success:
                        // Successful authentication. 
                        result = webAuthenticationResult.ResponseData.ToString();
                        break;
                    case WebAuthenticationStatus.ErrorHttp:
                        // HTTP error. 
                        result = webAuthenticationResult.ResponseErrorDetail.ToString();
                        break;
                    default:
                        // Other error.
                        result = webAuthenticationResult.ResponseData.ToString();
                        break;
                }
            }
            catch (Exception ex)
            {
                // Authentication failed. Handle parameter, SSL/TLS, and Network Unavailable errors here. 
                result = ex.Message;
            }
        }

        #endregion

        #region internal http methods
        protected async Task<TClass> GetRequest<TClass>(string route, bool isAuthRequired = false)
			where TClass : class
		{
			using (var client = new HttpClient())
			{
                if (isAuthRequired)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Utility.Authentication.Base64Encode($"{Username}:{Password}"));
                }
                var response = await client.GetAsync($"{WordPressUri}{route}").ConfigureAwait(false);
                
                if (response.IsSuccessStatusCode)
				{
					var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
					return JsonConvert.DeserializeObject<TClass>(responseString);
				}
			}
			return default(TClass);
		}

        protected async Task<TClass> PostRequest<TClass>(string route, StringContent postBody)
            where TClass : class
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Utility.Authentication.Base64Encode($"{Username}:{Password}"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsync($"{WordPressUri}{route}", postBody).ConfigureAwait(false);
                
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return JsonConvert.DeserializeObject<TClass>(responseString);
                }
            }
            return default(TClass);
        }

        #endregion

    }
}