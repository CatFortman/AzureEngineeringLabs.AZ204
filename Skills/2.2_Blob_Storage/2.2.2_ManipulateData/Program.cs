using Azure.Storage.Blobs;

namespace ManipulateData
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Copy items between containers demo");
            Task.Run(async () => await CopyBlobsBetweenContainers()).Wait();
            Console.WriteLine("Move items between storage accounts demo");
            Task.Run(async () => await MoveBlobsBetweenStorageAccounts()).Wait();
        }

        private static async Task  CopyBlobsBetweenContainers()
        {
            try
            {
                AppSettings appSettings = AppSettings.LoadFromFile();

                // Create blob client for source using SAS connection strings
                BlobServiceClient sourceBlobClient = Common.CreateBlobClientStorageFromSAS(appSettings.SourceSASConnectionString ?? throw new Exception("Source SAS Connection String is missing."));

                // create blob container client for source and destination
                BlobContainerClient sourceContainerClient = sourceBlobClient.GetBlobContainerClient(appSettings.SourceContainerName ?? throw new Exception("Source Container Name is missing."));
                BlobContainerClient destinationContainerClient = sourceBlobClient.GetBlobContainerClient(appSettings.DestinationContainerName ?? throw new Exception("Destination Container Name is missing."));

                // Ensure the destination container exists
                destinationContainerClient.CreateIfNotExists();

                // List blobs in the source container and copy them to the destination container
                foreach (var blobItem in sourceContainerClient.GetBlobs())
                {
                    string blobName = blobItem.Name;
                    BlobClient sourceBlob = sourceContainerClient.GetBlobClient(blobName);
                    BlobClient destinationBlob = destinationContainerClient.GetBlobClient(blobName);

                    // Start the copy operation
                    destinationBlob.StartCopyFromUri(sourceBlob.Uri);
                    Console.WriteLine($"Started copying blob: {blobName}");
                }

                Console.WriteLine("Copy operation initiated for all blobs.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private static async Task MoveBlobsBetweenStorageAccounts()
        {
            try
            {
                AppSettings appSettings = AppSettings.LoadFromFile();

                // Create blob clients for source and destination using SAS connection strings
                BlobServiceClient sourceBlobClient = Common.CreateBlobClientStorageFromSAS(appSettings.SourceSASConnectionString ?? throw new Exception("Source SAS Connection String is missing."));
                BlobServiceClient destinationBlobClient = Common.CreateBlobClientStorageFromSAS(appSettings.DestinationSASConnectionString ?? throw new Exception("Destination SAS Connection String is missing."));

                BlobContainerClient sourceContainerClient = sourceBlobClient.GetBlobContainerClient(appSettings.SourceContainerName ?? throw new Exception("Source Container Name is missing."));
                BlobContainerClient destinationContainerClient = destinationBlobClient.GetBlobContainerClient(appSettings.DestinationContainerName ?? throw new Exception("Destination Container Name is missing."));

                // Ensure the destination container exists
                destinationContainerClient.CreateIfNotExists();

                // List blobs in the source container and copy them to the destination container
                foreach (var blobItem in sourceContainerClient.GetBlobs())
                {
                    string blobName = blobItem.Name;
                    BlobClient sourceBlob = sourceContainerClient.GetBlobClient(blobName);
                    BlobClient destinationBlob = destinationContainerClient.GetBlobClient(blobName);

                    // Start the copy operation
                    destinationBlob.StartCopyFromUri(sourceBlob.Uri);
                    Console.WriteLine($"Started copying blob: {blobName}");
                }

                Console.WriteLine("Copy operation initiated for all blobs.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}