using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;

namespace GenerateSASToken;

class Program
{
    static void Main(string[] args)
    {
        string storageAccount = "az204testingcf9d";
        string containerName = "az204-blob-testing";
        string blobName = System.IO.Path.GetRandomFileName();


        DateTimeOffset startTimeKey = DateTimeOffset.UtcNow;;
        DateTimeOffset endTimeKey = DateTimeOffset.UtcNow.AddDays(7);
        DateTimeOffset startTimeSAS = startTimeKey;
        DateTimeOffset endTimeSAS = startTimeKey.AddYears(1);

        Uri blobEndpointUri = new Uri($"https://{storageAccount}.blob.core.windows.net");

        var defaultCredentials = new DefaultAzureCredential(true);

        BlobServiceClient blobClient = new BlobServiceClient(blobEndpointUri, defaultCredentials);

        // Get the key. We are going to use this key for creating the SAS
        UserDelegationKey userDelegationKey = blobClient.GetUserDelegationKey(startTimeKey, endTimeKey);

        System.Console.WriteLine($"User key starts on: {userDelegationKey.SignedStartsOn}");
        System.Console.WriteLine($"User key expires on: {userDelegationKey.SignedExpiresOn}");
        System.Console.WriteLine($"User key signed service: {userDelegationKey.SignedService}");
        System.Console.WriteLine($"User key signed version: {userDelegationKey.SignedVersion}");

        // We need to use the BlobSasBuilder for creating the SAS
        BlobSasBuilder sasBuilder = new BlobSasBuilder()
        {
            BlobContainerName = containerName,
            BlobName = blobName,
            Resource = "b", // b for blob, c for container
            StartsOn = startTimeSAS,
            ExpiresOn = endTimeSAS,
            Protocol = SasProtocol.Https
        }; 

        // We set the permissions Create, List, Add, Read, and Write
        sasBuilder.SetPermissions(BlobSasPermissions.All);

        string sasToken = sasBuilder.ToSasQueryParameters(userDelegationKey, storageAccount).ToString();

        System.Console.WriteLine($"SAS token: {sasToken}");

        // We can now use the SAS token to create a BlobClient and upload a blob
        UriBuilder uriBuilder = new UriBuilder(blobEndpointUri)
        {
            Scheme = "https",
            Host = $"{storageAccount}.blob.core.windows.net",
            Path = $"{containerName}/{blobName}",
            Query = sasToken
        };

        // We create a random text file to upload
        using (System.IO.StreamWriter sw = System.IO.File.CreateText(blobName))
        {
            sw.WriteLine("This is a test file for uploading to Azure Blob Storage using a SAS token.");
        }

        BlobClient blobClientWithSAS = new BlobClient(uriBuilder.Uri);
        blobClientWithSAS.Upload(blobName, true);

        System.Console.WriteLine($"Blob uploaded successfully with SAS token. Blob URI: {uriBuilder.Uri}");

        // Now we download the blob using the same SAS token to verify that it works
        Console.WriteLine($"Reading content from test blob {blobName}");
        Console.WriteLine();

        BlobDownloadInfo downloadInfo = blobClientWithSAS.Download();

        using (StreamReader reader = new StreamReader(downloadInfo.Content, true))
        {
            string content = reader.ReadToEnd();
            Console.WriteLine(content);
        }

        Console.WriteLine();
        Console.WriteLine("Blob read successfully with SAS token.");
    }
}