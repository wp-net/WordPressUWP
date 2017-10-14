using Microsoft.Toolkit.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordPressPCL.Models;
using System.Threading;
using WordPressUWP.Interfaces;
using GalaSoft.MvvmLight.Ioc;

namespace WordPressUWP.Services
{
    public class PostsService : IIncrementalSource<Post>
    {
        private IWordPressService _wordPressService;

        public PostsService()
        {
            _wordPressService = SimpleIoc.Default.GetInstance<IWordPressService>();
        }

        public async Task<IEnumerable<Post>> GetPagedItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _wordPressService.GetLatestPosts(pageIndex, pageSize);
        }
    }
}
