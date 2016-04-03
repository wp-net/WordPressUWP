using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WordPressUWP;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WordPressUWPTestApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            TestPosts();
        }

        public async void TestPosts()
        {
            var client = new WordPressClient(ApiCredentials.WordPressUri);
            client.Username = ApiCredentials.Username;
            client.Password = ApiCredentials.Password;

            var posts = await client.ListPosts();
            var post = await client.GetPost("1");
           
            var comments = await client.ListComments();
            var comment = await client.GetComment("3");

            //var currentUser = await client.GetCurrentUser();
        }
    }
}
