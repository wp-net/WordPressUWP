namespace WordPressUWP.Helpers
{
    // Rename ApiCredentialsSample to ApiCredentials
    // public static class ApiCredentials
    public static class ApiCredentialsSample
    {
        public const string BaseUri = "http://yoursite.com";
        public static string WordPressUri = "http://yoursite.com/wp-json/";

        // Push Notification Settings
        public const string HubName = "HubName";
        public const string AccessSiganture = "Endpoint=sb://*";
    }
}
