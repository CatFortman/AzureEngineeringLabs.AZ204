using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace StorageTiers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Copy items between containers demo");
            Task.Run(async () => await SetBlobTiers()).Wait();
        }

        private static async Task SetBlobTiers()
        {
            try
            {
                AppSettings appSettings = AppSettings.LoadFromFile();

                // Create blob client for source using SAS connection strings
                BlobServiceClient sourceBlobClient = Common.CreateBlobClientStorageFromSAS(appSettings.SASConnectionString ?? throw new Exception("Source SAS Connection String is missing."));

                // create blob container client for source and destination
                BlobContainerClient sourceContainerClient = sourceBlobClient.GetBlobContainerClient(appSettings.ContainerName ?? throw new Exception("Source Container Name is missing."));

                string blobName = "test1.zip";
                BlobClient sourcebBlobClient = sourceContainerClient.GetBlobClient(blobName);

                // Get current access tier of the blob
                var properties = await sourcebBlobClient.GetPropertiesAsync();
                Console.WriteLine($@"Current Access Tier of blob 
                    '{blobName}': 
                    Access Tier : {properties.Value.AccessTier}
                    Inferred Access Tier : {properties.Value.AccessTierInferred}
                    Date Access Tier Changed : {properties.Value.AccessTierChangedOn}");

                // Set access tier to Cool
                await sourcebBlobClient.SetAccessTierAsync(AccessTier.Cool);

                // Get current access tier of the blob
                properties = await sourcebBlobClient.GetPropertiesAsync();
                Console.WriteLine($@"Current Access Tier of blob 
                    '{blobName}': 
                    Access Tier : {properties.Value.AccessTier}
                    Inferred Access Tier : {properties.Value.AccessTierInferred}
                    Date Access Tier Changed : {properties.Value.AccessTierChangedOn}");

                // Set access tier to Cool
                await sourcebBlobClient.SetAccessTierAsync(AccessTier.Archive);

                // Get current access tier of the blob
                properties = await sourcebBlobClient.GetPropertiesAsync();
                Console.WriteLine($@"Current Access Tier of blob 
                    '{blobName}': 
                    Access Tier : {properties.Value.AccessTier}
                    Inferred Access Tier : {properties.Value.AccessTierInferred}
                    Date Access Tier Changed : {properties.Value.AccessTierChangedOn}
                    Archive Status : {properties.Value.ArchiveStatus}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

    }
}