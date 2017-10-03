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

namespace WordPressUWP.ViewModels
{
    public class NewsViewModel : ViewModelBase
    {
        private IWordPressService _wordPressService;
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
        
        public ObservableCollection<Post> Posts { get; private set; } = new ObservableCollection<Post>();
        private Post _selectedPost;

        public Post SelectedPost
        {
            get { return _selectedPost; }
            set { Set(ref _selectedPost, value); }
        }

        public NewsViewModel(IWordPressService wordPressService)
        {
            _wordPressService = wordPressService;
        }

        public async Task LoadDataAsync(VisualState currentState)
        {
            _currentState = currentState;
            
            Posts.Clear();
            var posts = await _wordPressService.GetLatestPosts();
            foreach(var item in posts)
            {
                Posts.Add(item);
            }
            SelectedPost = Posts.First();

            Debug.WriteLine(posts.Count());
        }

        private void OnStateChanged(VisualStateChangedEventArgs args)
        {
            _currentState = args.NewState;
        }

        private void OnItemClick(ItemClickEventArgs args)
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
                }
            }
        }
    }
}
