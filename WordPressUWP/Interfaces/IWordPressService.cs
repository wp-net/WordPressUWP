using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordPressPCL.Models;

namespace WordPressUWP.Interfaces
{
    public interface IWordPressService
    {
        Task<IEnumerable<Post>> GetLatestPosts();
    }
}
