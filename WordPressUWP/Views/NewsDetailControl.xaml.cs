using System;
using System.Diagnostics;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WordPressPCL.Models;
using WordPressUWP.Helpers;

namespace WordPressUWP.Views
{
    public sealed partial class NewsDetailControl : UserControl
    {
        public Post MasterMenuItem
        {
            get { return GetValue(MasterMenuItemProperty) as Post; }
            set { SetValue(MasterMenuItemProperty, value); }
        }

        public static readonly DependencyProperty MasterMenuItemProperty = DependencyProperty.Register("MasterMenuItem", typeof(Post), typeof(NewsDetailControl), new PropertyMetadata(null));

        public delegate void SwipeEventHandler(object sender, SwipedEventArgs e);

        public event SwipeEventHandler Swiped;

        public NewsDetailControl()
        {
            InitializeComponent();
        }

        private async void WebView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            if (args.Uri == null)
                return;
            var s = args.Uri.ToString();
            args.Cancel = true;
            await Launcher.LaunchUriAsync(new Uri(s));

            // TODO Show image popup

            //if (s.Contains(".jpg") || s.Contains(".jpeg") || s.Contains(".png") || s.Contains(".gif"))
            //{
            //    args.Cancel = true;
            //    Debug.WriteLine(s);
            //    //WindowWrapper.Current().Dispatcher.Dispatch(() =>
            //    //{
            //    //    var modal = Window.Current.Content as ModalDialog;
            //    //    modal.CanBackButtonDismiss = true;
            //    //    modal.DisableBackButtonWhenModal = false;
            //    //    var view = modal.ModalContent as Busy;
            //    //    if (view == null)
            //    //        modal.ModalContent = view = new Busy();
            //    //    modal.IsModal = view.IsBusy = true;
            //    //    view.BusyText = "text";
            //    //});
            //}
        }

        private void PostWebView_ScriptNotify(object sender, NotifyEventArgs e)
        {
            SwipeDirection direction;
            if (e.Value.Equals("swipeleft"))
                direction = SwipeDirection.Left;
            else
                direction = SwipeDirection.Right;

            Swiped?.Invoke(this, new SwipedEventArgs(direction));
        }
    }
}
