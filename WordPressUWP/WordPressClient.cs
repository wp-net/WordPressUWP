using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordPressUWP.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;
using Newtonsoft.Json;

namespace WordPressUWP
{
    public class WordPressClient
    {
        public String Endpoint { get; set; }

        public WordPressClient(String endpoint)
        {
            Endpoint = endpoint;
        }


        public async Task<List<Post>> ListPosts(int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date)
        {
            List<Post> result = null;
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{Endpoint}posts");
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<Post>>(responseString);
                }
            }
            return result;
        }

        public async Task<Post> GetPost(String id)
        {
            Post result = null;
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{Endpoint}posts/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<Post>(responseString);
                }
            }
            return result;
        }

        public async Task<List<Comment>> ListComments()
        {
            List<Comment> result = null;
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{Endpoint}comments");
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<Comment>>(responseString);
                }
            }
            return result;
        }

        public async Task<Comment> GetComment(String id)
        {
            Comment result = null;
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{Endpoint}comment/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<Comment>(responseString);
                }
            }
            return result;
        }



    }
}
