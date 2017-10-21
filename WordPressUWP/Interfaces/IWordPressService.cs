using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WordPressPCL.Models;

namespace WordPressUWP.Interfaces
{
    public interface IWordPressService
    {
        Task<IEnumerable<Post>> GetLatestPosts(int page = 0, int perPage = 20);

        Task<bool> AuthenticateUser(string username, string password);

        Task<bool> IsUserAuthenticated();

        Task<User> GetUser();

        Task<List<CommentThreaded>> GetCommentsForPost(int postid);

        Task<Comment> PostComment(int postId, string text);
        Task<bool> Logout();
    }
}
