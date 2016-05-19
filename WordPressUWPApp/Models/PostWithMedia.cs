using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordPressUWP.Models;

namespace WordPressUWPApp.Models
{
    public class PostWithMedia
    {
        public Post Post;
        public Media FeaturedMediaFull { get; set; }
    }
}
