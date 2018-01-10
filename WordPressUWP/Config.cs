using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordPressUWP
{
    public static class Config
    {
        public const string BaseUri = "http://api.medienstudio.net/";
        public static string WordPressUri = $"{BaseUri}wp-json/";

        // Push Notification Settings
        public const string HubName = "NotificationHubName";
        public const string AccessSiganture = "Endpoint=";

        // Comments
        public static int CommentDepth = 3;
    }
}
