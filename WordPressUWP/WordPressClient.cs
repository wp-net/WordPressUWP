using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<IList<Post>> ListPosts(int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date)
		{
			return await Download<Post[]>($"posts").ConfigureAwait(false);
		}

		public async Task<Post> GetPost(String id)
		{
			return await Download<Post>($"posts/{id}").ConfigureAwait(false);
		}

		public async Task<IList<Comment>> ListComments()
		{
			return await Download<Comment[]>("comments").ConfigureAwait(false);
		}

		public async Task<Comment> GetComment(string id)
		{
			return await Download<Comment>($"comment/{id}").ConfigureAwait(false);
		}
        public async Task<User> GetCurrentUser()
        {
            return await Download<User>($"users/me", true).ConfigureAwait(false);
        }

        protected async Task<TClass> Download<TClass>(string section, bool isAuthRequired = false)
			where TClass : class
		{
			using (var client = new HttpClient())
			{
                if (isAuthRequired)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Utility.Authentication.Base64Encode($"{Username}:{Password}"));
                }
                var response = await client.GetAsync($"{WordPressUri}{section}").ConfigureAwait(false);
                
                if (response.IsSuccessStatusCode)
				{
					var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
					return JsonConvert.DeserializeObject<TClass>(responseString);
				}
			}
			return default(TClass);
		}

    }
}