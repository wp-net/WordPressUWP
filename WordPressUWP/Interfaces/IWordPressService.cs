using System.Collections.Generic;
using System.Threading.Tasks;
using WordPressPCL.Models;

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
