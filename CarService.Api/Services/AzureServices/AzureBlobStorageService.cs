using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace CarService.Api.Services.AzureServices
{
    public class AzureBlobStorageService : IAzureBlobStorageService
    {
        StorageCredentials storageCredentials;
        CloudStorageAccount cloudStorageAccount;
        CloudBlobClient cloudBlobClient;
        public AzureBlobStorageService(AzureBlobStorageSettings settings)
        {
            storageCredentials = new StorageCredentials(settings.StorageAccount, settings.StorageKey);
            cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
        }

        public async Task<string> UploadFile(MemoryStream stream, string containerName, string fileName)
        {
            var container = cloudBlobClient.GetContainerReference(containerName);
            await container.CreateIfNotExistsAsync();

            BlobContainerPermissions permissions = await container.GetPermissionsAsync();
            permissions.PublicAccess = BlobContainerPublicAccessType.Container;
            
            await container.SetPermissionsAsync(permissions);

            var newBlob = container.GetBlockBlobReference(fileName);
            await newBlob.UploadFromStreamAsync(stream);

            return newBlob.Uri.AbsoluteUri;
        }
    }
}