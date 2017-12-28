# WordPressUWP

[![Join the chat at https://gitter.im/ThomasPe/WordPressUWP](https://badges.gitter.im/ThomasPe/WordPressUWP.svg)](https://gitter.im/ThomasPe/WordPressUWP?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

This is a Universal Windows Platform app framework designed to turn WordPress Blogs / Sites into nice little apps. It's built on
* Windows Template Studio
* [WordPressPCL (WordPress REST API Wrapper)](https://github.com/ThomasPe/WordPressPCL)

## Features
working and planned features for WordPressUWP:
- [x] Show posts
- [x] Show comments
- [x] Settings page
- [x] Sign In
- [x] Add comment
- [ ] Push Notifications (new posts, comments)
- [ ] Live Tiles
- [x] Continuum Support
- [ ] Enable Ads

# Quickstart

## WordPress Plugins
Since WordPress 4.7 the REST API has been integrated into the core so there's no need for any plugins to get basic functionality. If you want to access protected endpoints, this library supports authentication through JSON Web Tokens (JWT) (plugin required).

* [WordPress 4.7 or newer](https://wordpress.org/)
* [JWT Authentication for WP REST API](https://wordpress.org/plugins/jwt-authentication-for-wp-rest-api/)

## Getting Started

Just clone or download the repo and open it in Visual Studio. Before you can build you'll need to create a `ApiCredentials.cs` class inside the `WordPressUWPApp.Utility` folder. Here you need to enter your site uri.

```c#
namespace WordPressUWPApp.Utility
{
    public static class ApiCredentials
    {
        public static string WordPressUri = "http://yoursite.com/wp-json/";
    }

}

```

## Hall of Fame

This is a list of apps based on the WordPressUWP framework. Feel free to add yours!
- [WindowsArea](https://www.microsoft.com/de-de/store/p/windowsarea/9n9zxm79mqr7)
