using Windows.UI.Xaml;
using System.Threading.Tasks;
using WordPressUWPApp.Services.SettingsServices;
using Windows.ApplicationModel.Activation;
using Template10.Controls;
using Template10.Common;
using System;
using System.Linq;
using Windows.Foundation.Metadata;
using Windows.UI;

namespace WordPressUWPApp
{
    /// Documentation on APIs used in this page:
    /// https://github.com/Windows-XAML/Template10/wiki

    sealed partial class App : Template10.Common.BootStrapper
    {
        public App()
        {
            InitializeComponent();
            SplashFactory = (e) => new Views.Splash(e);

            #region App settings

            var _settings = SettingsService.Instance;
            RequestedTheme = _settings.AppTheme;
            CacheMaxDuration = _settings.CacheMaxDuration;
            ShowShellBackButton = _settings.UseShellBackButton;

            #endregion
        }

        public override async Task OnInitializeAsync(IActivatedEventArgs args)
        {
            if (Window.Current.Content as ModalDialog == null)
            {
                // create a new frame 
                var nav = NavigationServiceFactory(BackButton.Attach, ExistingContent.Include);
                // create modal root
                Window.Current.Content = new ModalDialog
                {
                    DisableBackButtonWhenModal = true,
                    Content = new Views.Shell(nav),
                    ModalContent = new Views.Busy(),
                };
            }

            //PC customization
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationView"))
            {

                var titleBar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;
                if (titleBar != null)
                {
                    titleBar.BackgroundColor = (Color)App.Current.Resources["PageHeaderBackground"];
                    titleBar.ForegroundColor = Colors.Black;
                    titleBar.ButtonBackgroundColor = (Color)App.Current.Resources["PageHeaderBackground"];
                    titleBar.ButtonForegroundColor = Colors.Black;
                }
            }

            //Mobile customization
            //if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            //{
            //    StatusBar.Set
            //    var statusBar = StatusBar.GetForCurrentView();
            //    if (statusBar != null)
            //    {
            //        statusBar.BackgroundOpacity = 1;
            //        statusBar.BackgroundColor = Colors.DarkBlue;
            //        statusBar.ForegroundColor = Colors.White;
            //    }
            //}
            await Task.CompletedTask;
        }

        public override async Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            // long-running startup tasks go here

            NavigationService.Navigate(typeof(Views.MainPage));
            await Task.CompletedTask;

        }
    }
}

