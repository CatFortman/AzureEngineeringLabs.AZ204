using System.Text;
using Azure.Messaging.EventGrid;

var data = new
{
    name = "WIN_20250118_21_33_51_Pro.jpg",
    url = "http://127.0.0.1:10000/devstoreaccount1/images/WIN_20250118_21_33_51_Pro.jpg"
};

var evt = new EventGridEvent(
    subject: "/blobServices/default/containers/images/blobs/WIN_20250118_21_33_51_Pro.jpg",
    eventType: "Microsoft.Storage.BlobCreated",
    dataVersion: "1.0",
    data: BinaryData.FromObjectAsJson(data)
);

var payload = BinaryData.FromObjectAsJson(new[] { evt }).ToString();

var client = new HttpClient();

var content =   new StringContent(payload, Encoding.UTF8, "application/json");
content.Headers.Add("aeg-event-type", "Notification");

var response = await client.PostAsync(
    "http://localhost:7071/runtime/webhooks/EventGrid?functionName=BlobEventProcessor",
    content
);
Console.WriteLine(response.StatusCode);
Console.WriteLine(string.Join("\n", response.Headers));
Console.WriteLine(await response.Content.ReadAsStringAsync());