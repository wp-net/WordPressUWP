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

        private Post _item;

        public Post Item
        {
            get { return _item; }
            set { Set(ref _item, value);}
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

        public NewsDetailViewModel(IWordPressService wordPressService, IInAppNotificationService inAppNotificationService)
        {
            _wordPressService = wordPressService;
            _inAppNotificationService = inAppNotificationService;
        }

        internal async Task Init()
        {
            await GetComments(Item.Id);
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
            await GetComments(Item.Id);
        }

        public async Task PostComment()
        {
            if (await _wordPressService.IsUserAuthenticated())
            {
                var comment = await _wordPressService.PostComment(Item.Id, CommentInput);
                if (comment != null)
                {
                    _inAppNotificationService.ShowInAppNotification("successfully posted comment");
                    CommentInput = String.Empty;
                    await GetComments(Item.Id);
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
    }
}
