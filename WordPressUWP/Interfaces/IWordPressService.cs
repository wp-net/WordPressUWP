using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressUWP.Helpers;

namespace WordPressUWP.Interfaces
{
    public interface IWordPressService
    {
        bool IsAuthenticated { get; set; }

        bool IsLoadingPosts { get; set; }

        Task<IEnumerable<Post>> GetLatestPosts(int page = 0, int perPage = 20);

        Task<bool> AuthenticateUser(string username, string password);

        Task<bool> IsUserAuthenticated();

        User GetUser();

        Task<List<CommentThreaded>> GetCommentsForPost(int postid);

        Task<Comment> PostComment(int postId, string text, int replyto = 0);
        Task<bool> Logout();
    }
}
