using WordPressUWPApp.ViewModels;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls;
using System.Diagnostics;
using Template10.Controls;
using Windows.UI.Xaml;
using Template10.Common;

namespace WordPressUWPApp.Views
{
    public sealed partial class DetailPage : Page
    {
        public DetailPage()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Disabled;
        }

        private void WebView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            if (args.Uri == null)
                return;
            var s = args.Uri.ToString();
            if (s.Contains(".jpg") || s.Contains(".jpeg") ||s.Contains(".png") || s.Contains(".gif")){
                args.Cancel = true;
                Debug.WriteLine(s);
                WindowWrapper.Current().Dispatcher.Dispatch(() =>
                {
                    var modal = Window.Current.Content as ModalDialog;
                    modal.CanBackButtonDismiss = true;
                    modal.DisableBackButtonWhenModal = false;
                    var view = modal.ModalContent as Busy;
                    if (view == null)
                        modal.ModalContent = view = new Busy();
                    modal.IsModal = view.IsBusy = true;
                    view.BusyText = "text";
                });
            }
        }
    }
}

