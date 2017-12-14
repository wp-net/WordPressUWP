function call_the_endpoint($new_status, $old_status, $post){
        // Store the title into the array
        $data['title'] = get_the_title();
        // If there is a post thumbnail, get the link
        if (has_post_thumbnail()) {
            $data['thumbnail'] = get_the_post_thumbnail_url( get_the_ID(),'large' );
        }
        $data['id'] = get_the_id();
        $data['author'] =  get_the_author();
        $data['datetime'] = get_post_time('U', true);

        // Encode the data to be sent
        $json_data = json_encode($data);
        // Initiate the cURL
        curl_setopt($url, CURLOPT_CUSTOMREQUEST, "POST");
        curl_setopt($url, CURLOPT_POSTFIELDS, $json_data);
        curl_setopt($url, CURLOPT_RETURNTRANSFER, true);
        curl_setopt($url, CURLOPT_HTTPHEADER, array(
            'Content-Type: application/json',
            'Content-Length: ' . strlen($json_data))
        );
        // The results of our request, to use later if we want.
        $result = curl_exec($url);
    }
}
add_action('transition_post_status', 'call_the_endpoint',10,3);
