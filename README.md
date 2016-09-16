# WordPressUWP

[![Join the chat at https://gitter.im/ThomasPe/WordPressUWP](https://badges.gitter.im/ThomasPe/WordPressUWP.svg)](https://gitter.im/ThomasPe/WordPressUWP?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

This is a Universal Windows Platform app framework designed to turn WordPress Blogs / Sites into nice little apps. It's built on
* [Template10](https://github.com/Windows-XAML/Template10/wiki)
* [WordPressPCL (WordPress REST API Wrapper)](https://github.com/ThomasPe/WordPressPCL)

#Features
working and planned features for WordPressUWP:
- [x] Show posts
- [x] Show comments
- [x] Settings page
- [ ] Sign In
- [ ] Add comment
- [ ] Push Notifications (new posts, comments)
- [ ] Live Tiles
- [x] Continuum Support
- [ ] Enable Ads

#Quickstart

## WordPress Plugins
As the WP REST API (Version 2) Plugin is currently being integrated into WordPress core you'll still need to install the plugin on your site for the app to work. Also, there are two additional plugins for authentication.

* [WordPress REST API (Version 2)](https://wordpress.org/plugins/rest-api/)
* [Basic Authentication handler](https://github.com/WP-API/Basic-Auth)
* [WP REST API - OAuth 1.0a Server](https://github.com/WP-API/OAuth1)

## Getting Started

Just clone or download the repo and open it in Visual Studio. Before you can build you'll need to create a `ApiCredentials.cs` class inside the `WordPressUWPApp.Utility` folder. Here you need to enter your site uri and admin credentials (if you want to do stuff that needs admin rights from your app).

```c#
namespace WordPressUWPApp.Utility
{
    public static class ApiCredentials
    {
        public static string WordPressUri = "http://yoursite.com/wp-json/wp/v2/";
        public static string Username = "Admin";
        public static string Password = "Password";
    }

}

```
    

The full documentation will be made available here (but isn't yet...) 
http://thomaspe.github.io/WordPressUWP
