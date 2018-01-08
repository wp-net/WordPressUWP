#r "Microsoft.Azure.NotificationHubs"
#r "Newtonsoft.Json"

using System.Net;
using Microsoft.Azure.NotificationHubs;
using Newtonsoft.Json;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log, IAsyncCollector<Notification> notification)
{
    log.Info("C# HTTP trigger function processed a request.");

    dynamic data = await req.Content.ReadAsAsync<object>();

    // Set name to query string or body data
    string title = data?.title;
    string id = data?.id;
    string img = data?.thumbnail;
    string author = data?.author;
    
	if(!string.IsNullOrEmpty(title)){
        string wnsNotificationPayload = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                                        "<toast activationType=\"foreground\" launch=\""+ id + "\"><visual><binding template=\"ToastGeneric\">" +
                                            "<text id=\"1\">" + 
                                                title + 
                                            "</text>" +
                                        "<text id=\"1\">" + 
                                                author + 
                                            "</text>" +
                                            "<image src=\"" + img + "\"/>" +
                                        "</binding></visual></toast>";
        await notification.AddAsync(new WindowsNotification(wnsNotificationPayload));  
    }

    return title == null
        ? req.CreateResponse(HttpStatusCode.BadRequest, "Failure")
        : req.CreateResponse(HttpStatusCode.OK, "Push sent");
}
