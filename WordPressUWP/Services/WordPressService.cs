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

namespace WordPressUWP.Services
{
    public class WordPressService : IWordPressService
    {
        private WordPressClient _client;

        public WordPressService()
        {
            _client = new WordPressClient(ApiCredentials.WordPressUri);
        }

        public async Task<IEnumerable<Post>> GetLatestPosts()
        {
            return await _client.Posts.Query(new PostsQueryBuilder()
            {
                Page = 0,
                PerPage = 20
            });
        }
    }
}
