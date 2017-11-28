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
using System.Diagnostics;

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
            Debug.WriteLine("GetPagedItemsAsync");
            return await _wordPressService.GetLatestPosts(pageIndex, pageSize);
        }
    }
}
