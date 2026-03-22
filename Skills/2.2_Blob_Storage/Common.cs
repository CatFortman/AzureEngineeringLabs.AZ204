using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.Blob;

namespace BlobStorage
{
    public class Common
    {
        public static CloudBlobClient CreateBlobClientStorageFromSAS(string SAStoken, string accountName)
        {
            CloudStorageAccount cloudStorageAccount;
            CloudBlobClient cloudBlobClient;

            try
            {
                bool useHttps = true;
                StorageCredentials storageCredentials = new StorageCredentials(SAStoken);
                cloudStorageAccount = new CloudStorageAccount(storageCredentials, accountName, endpointSuffix: null, useHttps: useHttps);
                cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create blob client.", ex);
            }

            return cloudBlobClient;
        }
    }
}