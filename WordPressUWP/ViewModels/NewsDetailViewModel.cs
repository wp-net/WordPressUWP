using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Windows.UI.Xaml;
using WordPressUWP.Services;
using WordPressPCL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using WordPressUWP.Interfaces;
using System.Collections.ObjectModel;
using System;

namespace WordPressUWP.ViewModels
{
    public class NewsDetailViewModel : ViewModelBase
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

        private Post _selectedPost;

        public Post SelectedPost
        {
            get { return _selectedPost; }
            set { Set(ref _selectedPost, value);}
        }

        private ObservableCollection<CommentThreaded> _comments;
        public ObservableCollection<CommentThreaded> Comments
        {
            get { return _comments; }
            set { Set(ref _comments, value); }
        }

        private bool _isCommentsLoading;
        public bool IsCommentsLoading
        {
            get { return _isCommentsLoading; }
            set { Set(ref _isCommentsLoading, value); }
        }

        private string _commentInput;
        public string CommentInput
        {
            get { return _commentInput; }
            set { Set(ref _commentInput, value); }
        }

        private Comment _commentReply;
        public Comment CommentReply
        {
            get { return _commentReply; }
            set { Set(ref _commentReply, value); }
        }

        public NewsDetailViewModel(IWordPressService wordPressService, IInAppNotificationService inAppNotificationService)
        {
            _wordPressService = wordPressService;
            _inAppNotificationService = inAppNotificationService;
        }

        internal async Task Init()
        {
            await GetComments(SelectedPost.Id);
        }

        private void OnStateChanged(VisualStateChangedEventArgs args)
        {
            if (args.OldState.Name == NarrowStateName && args.NewState.Name == WideStateName)
            {
                NavigationService.GoBack();
            }
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

        public async void RefreshComments()
        {
            await GetComments(SelectedPost.Id);
        }

        public async Task PostComment()
        {
            if (await _wordPressService.IsUserAuthenticated())
            {
                int replyto = 0;
                if (CommentReply != null)
                    replyto = CommentReply.Id;
                var comment = await _wordPressService.PostComment(SelectedPost.Id, CommentInput, replyto);
                if (comment != null)
                {
                    _inAppNotificationService.ShowInAppNotification("successfully posted comment");
                    CommentInput = String.Empty;
                    await GetComments(SelectedPost.Id);
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

        public void CommentReplyUnset()
        {
            CommentReply = null;
        }
    }
}
