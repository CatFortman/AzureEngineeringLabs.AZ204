using Azure.Storage.Blobs;

namespace ManipulateData
{
    public class Common
    {
        public static BlobServiceClient CreateBlobClientStorageFromSAS(string saSConnectionString)
        {
            BlobServiceClient blobClient;

            try
            {
               blobClient = new BlobServiceClient(saSConnectionString);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create blob client.", ex);
            }

            return blobClient;
        }
    }
}