using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordPressPCL.Models;

namespace WordPressUWP.Models
{
    public class CommentThreaded : Comment
    {
        public int Depth { get; set; }
    }
}
