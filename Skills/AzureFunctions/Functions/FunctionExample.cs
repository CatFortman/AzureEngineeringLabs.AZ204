using Microsoft.Azure.Functions.Worker;

using Azure.Messaging.EventGrid;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Functions;

public class BlobEventProcessor
{
    private readonly ILogger _logger;

    public BlobEventProcessor(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<BlobEventProcessor>();
    }

    [Function("BlobEventProcessor")]
    public FunctionOutput Run(
            [EventGridTrigger] EventGridEvent eventGridEvent,
            [BlobInput("{data.url}", Connection = "ImagesBlobStorage")] Stream imageBlob)
    {
        _logger.LogInformation("Blob stream length: {Length} bytes", imageBlob.Length);

        var document = new
        {
            id = Guid.NewGuid(),
            description = eventGridEvent.Subject
        };

        _logger.LogInformation("Event processed: {subject}", eventGridEvent.Subject);

        var signalRMessage = new SignalRMessageAction("newMessage") { Arguments = new[] { document } };
        _logger.LogInformation("[MOCK] Would send to SignalR: {message}", JsonSerializer.Serialize(signalRMessage));

        _logger.LogInformation("Save document to Cosmos DB: {document}", document.description.ToString());
        return new FunctionOutput
        {
            CosmosDocument = document
        };
    }
}
public class FunctionOutput
{
    [CosmosDBOutput(
        databaseName: "GIS",
        containerName: "Processed_Images",
        Connection = "CosmosDBConnection")]
    public object CosmosDocument { get; set; }
}