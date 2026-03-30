namespace QueryMetadata
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                AppSettings appSettings = AppSettings.LoadFromFile();

                // Create blob client using SAS token
                var blobClient = Common.CreateBlobClientStorageFromSAS(appSettings.SASToken!, appSettings.StorageAccountName!);
                Console.WriteLine("Blob client created successfully.");

                // Get a container reference for the new blob container
                var container = blobClient.GetContainerReference(appSettings.ContainerName!);
                Console.WriteLine($"Container reference for '{appSettings.ContainerName}' obtained successfully.");
                // Create the container if it doesn't exist
                container.CreateIfNotExists();

                // You need to fetch the container properties before getting their values
                container.FetchAttributes();
                Console.WriteLine($"Container '{appSettings.ContainerName}' properties fetched successfully.");
                Console.WriteLine($"ETag: {container.Properties.ETag}");
                Console.WriteLine($"Last Modified: {container.Properties.LastModified}");
                Console.WriteLine($"Lease Status: {container.Properties.LeaseStatus}");

                Console.WriteLine("Blob storage operations completed successfully.");

                // Add some metadata to the container that we created
                container.Metadata.Add("department", "Finance");
                container.Metadata["category"] = "Confidential";
                container.Metadata.Add("docType", "Pdf Documents");

                // Save the metadata to the container in azure
                container.SetMetadata();

                // List newly added metadata. We need to fetch the container properties again to get the updated metadata values
                container.FetchAttributes();
                Console.WriteLine("Metadata for container:");
                foreach (var metadata in container.Metadata)
                {
                    Console.WriteLine($"{metadata.Key}: {metadata.Value}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}