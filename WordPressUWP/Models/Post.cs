using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordPressUWP.Models
{
    public class Post
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("date")]
        public DateTime date { get; set; }
        [JsonProperty("date_gmt")]
        public DateTime date_gmt { get; set; }
        [JsonProperty("guid")]
        public Guid guid { get; set; }
        [JsonProperty("modified")]
        public string modified { get; set; }
        [JsonProperty("modified_gmt")]
        public string modified_gmt { get; set; }
        [JsonProperty("slug")]
        public string slug { get; set; }
        [JsonProperty("type")]
        public string type { get; set; }
        [JsonProperty("link")]
        public string link { get; set; }
        [JsonProperty("title")]
        public Title title { get; set; }
        [JsonProperty("content")]
        public Content content { get; set; }
        [JsonProperty("excerpt")]
        public Excerpt excerpt { get; set; }
        [JsonProperty("author")]
        public int author { get; set; }
        [JsonProperty("featured_image")]
        public int featured_image { get; set; }
        [JsonProperty("comment_status")]
        public string comment_status { get; set; }
        [JsonProperty("ping_status")]
        public string ping_status { get; set; }
        [JsonProperty("sticky")]
        public bool sticky { get; set; }
        [JsonProperty("format")]
        public string format { get; set; }

        //public Links _links { get; set; }

        public enum OrderBy
        {
            date, id, include, title, slug
        }

    }
    public class Guid
    {
        [JsonProperty("rendered")]
        public string rendered { get; set; }
    }

    public class Title
    {
        [JsonProperty("rendered")]
        public string rendered { get; set; }
    }



    public class Excerpt
    {
        [JsonProperty("rendered")]
        public string rendered { get; set; }
    }



    public class About
    {
        [JsonProperty("href")]
        public string href { get; set; }
    }

    public class Author
    {
        [JsonProperty("embeddable")]
        public bool embeddable { get; set; }
        [JsonProperty("href")]
        public string href { get; set; }
    }

    public class Reply
    {
        [JsonProperty("embeddable")]
        public bool embeddable { get; set; }
        [JsonProperty("href")]
        public string href { get; set; }
    }

    public class VersionHistory
    {
        [JsonProperty("href")]
        public string href { get; set; }
    }

    public class HttpsApiWOrgFeaturedmedia
    {
        [JsonProperty("embeddable")]
        public bool embeddable { get; set; }
        [JsonProperty("href")]
        public string href { get; set; }
    }

    public class HttpsApiWOrgAttachment
    {
        [JsonProperty("href")]
        public string href { get; set; }
    }

    public class HttpsApiWOrgTerm
    {
        public string taxonomy { get; set; }
        [JsonProperty("embeddable")]
        public bool embeddable { get; set; }
        [JsonProperty("href")]
        public string href { get; set; }
    }

    public class HttpsApiWOrgMeta
    {
        [JsonProperty("embeddable")]
        public bool embeddable { get; set; }
        [JsonProperty("href")]
        public string href { get; set; }
    }

}
