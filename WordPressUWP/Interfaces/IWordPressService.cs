using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressUWP.Models;

namespace WordPressUWP.Interfaces
{
    public interface IWordPressService
    {
        Task<IEnumerable<Post>> GetLatestPosts();

        Task<bool> AuthenticateUser(string username, string password);

        Task<bool> IsUserAuthenticated();

        Task<User> GetUser();

        Task<List<CommentThreaded>> GetCommentsForPost(int postid);
    }
}
