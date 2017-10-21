using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WordPressUWP.ViewModels;

namespace WordPressUWP.Views
{
    public sealed partial class ShellPage : Page
    {
        private ShellViewModel ViewModel
        {
            get { return DataContext as ShellViewModel; }
        }

        public ShellPage()
        {
            InitializeComponent();
            DataContext = ViewModel;
            ViewModel.Initialize(shellFrame);            
        }

        private void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.InAppNotificationRaised += ViewModel_InAppNotificationRaised;
            Window.Current.SizeChanged += Current_SizeChanged;
            InitLoginPopup();
        }

        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            InitLoginPopup();
        }

        private void InitLoginPopup()
        {
            var windowWidth = Window.Current.Bounds.Width;
            var windowHeight = Window.Current.Bounds.Height;
            double popupWidth;
            double popupHeight;
            double gridWidth;
            double gridHeight;

            //if (windowWidth > 700)
            //{
            //    gridWidth = 300;
            //    gridHeight = 400;
            //}
            //else
            //{
            //    gridWidth = windowWidth;
            //    gridHeight = windowHeight;
            //}
            //LoginPopupGrid.Width = gridWidth;
            //LoginPopupGrid.Height = gridHeight;
            //LoginPopup.HorizontalOffset = (windowWidth / 2) - (gridWidth / 2);
            //LoginPopup.VerticalOffset = (windowHeight / 2) - (gridHeight / 2);

            popupHeight = windowHeight;
            popupWidth = windowWidth;

            if (windowWidth <= 700)
            {
                LoginPopupGrid.Width = windowWidth;
                LoginPopupGrid.Height = windowHeight;
            }

            LoginPopupWrapper.Width = popupWidth;
            LoginPopupWrapper.Height = popupHeight;

            LoginPopupGrid.VerticalAlignment = VerticalAlignment.Center;
            LoginPopupGrid.HorizontalAlignment = HorizontalAlignment.Center;
        }

        private void ViewModel_InAppNotificationRaised(object sender, string e)
        {
            GlobalInAppNotification.Show(e, 3000);
        }

        private void LoginBtn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.Login(UsernameTbx.Text, PasswordTbx.Password);
            PasswordTbx.Password = String.Empty;
        }

    }
}
