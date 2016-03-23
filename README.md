# WordPressUWP
This is a portable library for consuimg the WordPress REST-API in a Universal Windows Plattform App.

    var client = new WordPressClient();
    client.Endpoint = "http://demo.wp-api.org/wp-json/wp/v2/";
    var posts = await client.ListPosts();
