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


        public async Task<List<Post>> ListPosts()
        {
            List<Post> result = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Endpoint);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync("posts");
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("success");
                    var responseString = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<Post>>(responseString);
                }
            }
            return result;

        }

    }
}
