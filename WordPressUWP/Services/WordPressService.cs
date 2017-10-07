using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordPressPCL;
using WordPressPCL.Models;
using WordPressPCL.Utility;
using WordPressUWP.Helpers;
using WordPressUWP.Interfaces;
using WordPressUWP.Models;

namespace WordPressUWP.Services
{
    public class WordPressService : IWordPressService
    {
        private WordPressClient _client;

        public WordPressService()
        {
            _client = new WordPressClient(ApiCredentials.WordPressUri);
        }

        public async Task<bool> AuthenticateUser(string username, string password)
        {
            _client.AuthMethod = AuthMethod.JWT;
            await _client.RequestJWToken(username, password);
            return await _client.IsValidJWToken();
        }

        public async Task<List<CommentThreaded>> GetCommentsForPost(int postid)
        {
            var comments = await _client.Comments.Query(new CommentsQueryBuilder()
            {
                Posts = new int[] { postid },
                Page = 1,
                PerPage = 100
            });

            return ThreadedCommentsHelper.GetThreadedComments(comments);
        }

        public async Task<IEnumerable<Post>> GetLatestPosts()
        {
            return await _client.Posts.Query(new PostsQueryBuilder()
            {
                Page = 0,
                PerPage = 20,
                Embed = true
            });
        }

        public async Task<User> GetUser()
        {
            return await _client.Users.GetCurrentUser();
        }

        public async Task<bool> IsUserAuthenticated()
        {
            return await _client.IsValidJWToken();
        }


    }
}
