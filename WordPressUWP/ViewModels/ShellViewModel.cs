using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WordPressUWP.Helpers;
using WordPressUWP.Services;
using WordPressUWP.Interfaces;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Messaging;

namespace WordPressUWP.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        private const string PanoramicStateName = "PanoramicState";
        private const string WideStateName = "WideState";
        private const string NarrowStateName = "NarrowState";
        private const double WideStateMinWindowWidth = 640;
        private const double PanoramicStateMinWindowWidth = 1024;

        public NavigationServiceEx NavigationService
        {
            get
            {
                return ServiceLocator.Current.GetInstance<NavigationServiceEx>();
            }
        }

        private bool _isPaneOpen;

        public bool IsPaneOpen
        {
            get { return _isPaneOpen; }
            set { Set(ref _isPaneOpen, value); }
        }

        private SplitViewDisplayMode _displayMode = SplitViewDisplayMode.CompactInline;

        public SplitViewDisplayMode DisplayMode
        {
            get { return _displayMode; }
            set { Set(ref _displayMode, value); }
        }

        private object _lastSelectedItem;

        private ObservableCollection<ShellNavigationItem> _primaryItems = new ObservableCollection<ShellNavigationItem>();

        public ObservableCollection<ShellNavigationItem> PrimaryItems
        {
            get { return _primaryItems; }
            set { Set(ref _primaryItems, value); }
        }

        private ObservableCollection<ShellNavigationItem> _secondaryItems = new ObservableCollection<ShellNavigationItem>();

        public ObservableCollection<ShellNavigationItem> SecondaryItems
        {
            get { return _secondaryItems; }
            set { Set(ref _secondaryItems, value); }
        }

        private bool _isLoginPopupOpen;
        public bool IsLoginPopupOpen
        {
            get { return _isLoginPopupOpen; }
            set { Set(ref _isLoginPopupOpen, value); }
        }

        private bool _isLoggingIn;
        public bool IsLoggingIn
        {
            get { return _isLoggingIn; }
            set { Set(ref _isLoggingIn, value); }
        }

        private bool _showLoginError;
        public bool ShowLoginError
        {
            get { return _showLoginError; }
            set { Set(ref _showLoginError, value); }
        }

        private ICommand _openPaneCommand;

        public ICommand OpenPaneCommand
        {
            get
            {
                if (_openPaneCommand == null)
                {
                    _openPaneCommand = new RelayCommand(() => IsPaneOpen = !_isPaneOpen);
                }

                return _openPaneCommand;
            }
        }

        private ICommand _itemSelected;

        public ICommand ItemSelectedCommand
        {
            get
            {
                if (_itemSelected == null)
                {
                    _itemSelected = new RelayCommand<ItemClickEventArgs>(ItemSelected);
                }

                return _itemSelected;
            }
        }

        private ICommand _stateChangedCommand;
        private IWordPressService _wordPressService;

        public ICommand StateChangedCommand
        {
            get
            {
                if (_stateChangedCommand == null)
                {
                    _stateChangedCommand = new RelayCommand<VisualStateChangedEventArgs>(args => GoToState(args.NewState.Name));
                }

                return _stateChangedCommand;
            }
        }

        public event EventHandler<string> InAppNotificationRaised;

        public ShellViewModel(IWordPressService wordPressService)
        {
            _wordPressService = wordPressService;
        }

        private void GoToState(string stateName)
        {
            switch (stateName)
            {
                case PanoramicStateName:
                    DisplayMode = SplitViewDisplayMode.CompactInline;
                    break;
                case WideStateName:
                    DisplayMode = SplitViewDisplayMode.CompactInline;
                    IsPaneOpen = false;
                    break;
                case NarrowStateName:
                    DisplayMode = SplitViewDisplayMode.Overlay;
                    IsPaneOpen = false;
                    
                    break;
                default:
                    break;
            }
        }

        public void Initialize(Frame frame)
        {
            NavigationService.Frame = frame;
            NavigationService.Navigated += Frame_Navigated;
            PopulateNavItems();

            InitializeState(Window.Current.Bounds.Width);
        }


        private void InitializeState(double windowWith)
        {
            if (windowWith < WideStateMinWindowWidth)
            {
                GoToState(NarrowStateName);
            }
            else if (windowWith < PanoramicStateMinWindowWidth)
            {
                GoToState(WideStateName);
            }
            else
            {
                GoToState(PanoramicStateName);
            }
        }

        private void PopulateNavItems()
        {
            _primaryItems.Clear();
            _secondaryItems.Clear();

            // TODO WTS: Change the symbols for each item as appropriate for your app
            // More on Segoe UI Symbol icons: https://docs.microsoft.com/windows/uwp/style/segoe-ui-symbol-font
            // Or to use an IconElement instead of a Symbol see https://github.com/Microsoft/WindowsTemplateStudio/blob/master/docs/projectTypes/navigationpane.md
            // Edit String/en-US/Resources.resw: Add a menu item title for each page
            _primaryItems.Add(new ShellNavigationItem("Shell_News".GetLocalized(), Symbol.Home, typeof(NewsViewModel).FullName));


            if (Microsoft.Services.Store.Engagement.StoreServicesFeedbackLauncher.IsSupported())
            {
                _secondaryItems.Add(
                    ShellNavigationItem.ForAction(
                        "Feedback",
                        Symbol.Comment,
                        async() => {
                            var launcher = Microsoft.Services.Store.Engagement.StoreServicesFeedbackLauncher.GetDefault();
                            await launcher.LaunchAsync();
                        }));
            }
            _secondaryItems.Add(new ShellNavigationItem("Shell_Settings".GetLocalized(), Symbol.Setting, typeof(SettingsViewModel).FullName));

            if (Config.EnableLogin)
            {
                _secondaryItems.Add(
                ShellNavigationItem.ForAction(
                    "Shell_Me".GetLocalized(),
                    Symbol.Contact,
                    () => {
                        OpenLoginPopup();
                    }));
            }
        }

        private void ItemSelected(ItemClickEventArgs args)
        {
            if (DisplayMode == SplitViewDisplayMode.CompactOverlay || DisplayMode == SplitViewDisplayMode.Overlay)
            {
                IsPaneOpen = false;
            }
            Navigate(args.ClickedItem);
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            if (e != null)
            {
                var vm = NavigationService.GetNameOfRegisteredPage(e.SourcePageType);
                var navigationItem = PrimaryItems?.FirstOrDefault(i => i.ViewModelName == vm);
                if (navigationItem == null)
                {
                    navigationItem = SecondaryItems?.FirstOrDefault(i => i.ViewModelName == vm);
                }

                if (navigationItem != null)
                {
                    ChangeSelected(_lastSelectedItem, navigationItem);
                    _lastSelectedItem = navigationItem;
                }
            }
        }

        private void ChangeSelected(object oldValue, object newValue)
        {
            if (oldValue != null)
            {
                (oldValue as ShellNavigationItem).IsSelected = false;
            }

            if (newValue != null)
            {
                (newValue as ShellNavigationItem).IsSelected = true;
            }
        }

        private void Navigate(object item)
        {
            if (item is ShellNavigationItem navigationItem)
            {
                if (navigationItem.Action != null)
                {
                    navigationItem.Action.Invoke();
                }
                else
                {
                    NavigationService.Navigate(navigationItem.ViewModelName);
                }
            }
        }

        public void OpenLoginPopup()
        {

            IsLoginPopupOpen = true;
        }

        public void CloseLoginPopup()
        {
            IsLoginPopupOpen = false;
        }

        public async void Login(string username, string password)
        {
            IsLoggingIn = true;
            if(!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password))
            {
                var isAuth = await _wordPressService.AuthenticateUser(username, password);
                if (!isAuth)
                {
                    ShowLoginError = true;
                }
            }
            IsLoggingIn = false;
        }

        public async void Logout()
        {
            await _wordPressService.Logout();
        }
    }
}
