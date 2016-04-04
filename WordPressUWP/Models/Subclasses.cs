using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordPressUWP.Models
{
	public class Content
	{
        [JsonProperty("rendered")]
        public string Rendered { get; set; }
        [JsonProperty("raw")]
        public string Raw { get; set; }
    }

	public class Self
	{
		public string Href { get; set; }
	}

	public class Collection
	{
		public string Href { get; set; }
	}
}