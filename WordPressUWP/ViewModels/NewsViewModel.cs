using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WordPressUWP.Services;
using WordPressPCL.Models;
using WordPressUWP.Interfaces;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Toolkit.Uwp;

namespace WordPressUWP.ViewModels
{
    public class NewsViewModel : ViewModelBase
    {
        private IWordPressService _wordPressService;
        private IInAppNotificationService _inAppNotificationService;

        public NavigationServiceEx NavigationService
        {
            get
            {
                return Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<NavigationServiceEx>();
            }
        }

        private const string NarrowStateName = "NarrowState";
        private const string WideStateName = "WideState";

        private VisualState _currentState;

        private ICommand _itemClickCommand;

        public ICommand ItemClickCommand
        {
            get
            {
                if (_itemClickCommand == null)
                {
                    _itemClickCommand = new RelayCommand<ItemClickEventArgs>(OnItemClick);
                }

                return _itemClickCommand;
            }
        }

        private ICommand _stateChangedCommand;

        public ICommand StateChangedCommand
        {
            get
            {
                if (_stateChangedCommand == null)
                {
                    _stateChangedCommand = new RelayCommand<VisualStateChangedEventArgs>(OnStateChanged);
                }

                return _stateChangedCommand;
            }
        }

        public IncrementalLoadingCollection<PostsService, Post> Posts;

        private Post _selectedPost;
        public Post SelectedPost
        {
            get { return _selectedPost; }
            set { Set(ref _selectedPost, value); }
        }

        private ObservableCollection<CommentThreaded> _comments;
        public ObservableCollection<CommentThreaded> Comments
        {
            get { return _comments; }
            set { Set(ref _comments, value); }
        }

        private string _commentInput;

        public string CommentInput
        {
            get { return _commentInput; }
            set { Set(ref _commentInput, value); }
        }

        private bool _isCommentsLoading;
        public bool IsCommentsLoading
        {
            get { return _isCommentsLoading; }
            set { Set(ref _isCommentsLoading, value); }
        }

        public NewsViewModel(IWordPressService wordPressService, IInAppNotificationService inAppNotificationService)
        {
            _wordPressService = wordPressService;
            _inAppNotificationService = inAppNotificationService;
        }

        internal async Task Init(VisualState currentState)
        {
            await LoadDataAsync(currentState);
        }

        private async Task GetComments(int postid)
        {
            IsCommentsLoading = true;
            if (Comments != null)
            {
                Comments.Clear();
            }

            var comments = await _wordPressService.GetCommentsForPost(postid);
            if (comments != null)
            {
                Comments = new ObservableCollection<CommentThreaded>(comments);
            }
            IsCommentsLoading = false;
        }

        public async Task PostComment()
        {
            if (await _wordPressService.IsUserAuthenticated())
            {
                var comment = await _wordPressService.PostComment(SelectedPost.Id, CommentInput);
                if (comment != null)
                {
                    _inAppNotificationService.ShowInAppNotification("successfully posted comment");
                    CommentInput = String.Empty;
                }
                else
                {
                    _inAppNotificationService.ShowInAppNotification("something went wrong...");
                }
            }
            else
            {
                _inAppNotificationService.ShowInAppNotification("You have to log in first.");
            }
        }

        public async Task LoadDataAsync(VisualState currentState)
        {
            _currentState = currentState;

            Posts = new IncrementalLoadingCollection<PostsService, Post>();
        }

        private void OnStateChanged(VisualStateChangedEventArgs args)
        {
            _currentState = args.NewState;
        }

        private async void OnItemClick(ItemClickEventArgs args)
        {
            if (args?.ClickedItem is Post item)
            {
                if (_currentState.Name == NarrowStateName)
                {
                    NavigationService.Navigate(typeof(NewsDetailViewModel).FullName, item);
                }
                else
                {
                    SelectedPost = item;
                    await GetComments(item.Id);
                }
            }
        }

        public async void RefreshComments()
        {
            await GetComments(SelectedPost.Id);
        }
    }
}
