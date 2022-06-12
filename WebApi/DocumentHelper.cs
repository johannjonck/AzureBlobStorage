using Azure.Storage.Blobs;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace WebApi
{
    public class DocumentHelper
    {
        private readonly IConfiguration _configuration;
        private CloudStorageAccount _cloudStorageAccount;

        public DocumentHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> UploadBlob(string fileFullName)
        {
            using var stream = new MemoryStream(System.IO.File.ReadAllBytes(fileFullName).ToArray());
            var blobConfig = _configuration.GetSection("BlobStorage");
            var contentType = blobConfig.GetSection("FileContentType").Value;
            var containerName = blobConfig.GetSection("StorageContainer").Value;
            var connectionString = blobConfig.GetSection("ConnectionString").Value;
            var fileStream = File.OpenRead(fileFullName);
            var formFile = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(fileStream.Name))
            {
                Headers = new HeaderDictionary(),
                ContentType = contentType
            };

            _cloudStorageAccount = CloudStorageAccount.Parse(connectionString);

            var cloudBlobClient = _cloudStorageAccount.CreateCloudBlobClient();
            var cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);

            if (await cloudBlobContainer.CreateIfNotExistsAsync())
            {
                await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Off });
            }

            var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(formFile.FileName);
            cloudBlockBlob.Properties.ContentType = formFile.ContentType;

            try
            {
                await cloudBlockBlob.UploadFromStreamAsync(formFile.OpenReadStream());
                return cloudBlockBlob.Properties.Length >= 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Task<MemoryStream> DownloadBlobFromStorageAccount(string fileFullName)
        {
            var blobConfig = _configuration.GetSection("BlobStorage");
            var connectionString = blobConfig.GetSection("ConnectionString").Value;
            var containerName = blobConfig.GetSection("StorageContainer").Value;
            var blobName = blobConfig.GetSection("BlobName").Value;

            try
            {
                BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
                BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
                BlobClient blobClient = blobContainerClient.GetBlobClient(blobName);
                MemoryStream memoryStream = new MemoryStream();

                blobClient.DownloadTo(memoryStream);
                memoryStream.Position = 0;

                return Task.FromResult(memoryStream);
            }
            catch (Exception ex)
            {
                var task = new Task(() => { throw ex; });
                return (Task<MemoryStream>)task;
            }
        }

    }
}
