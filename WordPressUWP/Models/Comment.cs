using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordPressUWP.Models
{
    public class Comment
    {
        public int id { get; set; }
        public int post { get; set; }
        public int parent { get; set; }
        public int author { get; set; }
        public string author_name { get; set; }
        public string author_url { get; set; }
        public AuthorAvatarUrls author_avatar_urls { get; set; }
        public string date { get; set; }
        public string date_gmt { get; set; }
        public Content content { get; set; }
        public string link { get; set; }
        public string status { get; set; }
        public string type { get; set; }
        //public Links _links { get; set; }
    }
    public class AuthorAvatarUrls
    {
        [JsonProperty("24")]
        public string size24 { get; set; }
        [JsonProperty("48")]
        public string size48 { get; set; }
        [JsonProperty("96")]
        public string size96 { get; set; }
    }

}
