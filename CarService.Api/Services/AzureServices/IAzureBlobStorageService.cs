using System.IO;
using System.Threading.Tasks;

namespace CarService.Api.Services.AzureServices
{
    public interface IAzureBlobStorageService
    {
        Task<string> UploadFile(MemoryStream stream, string containerName, string fileName);
    }
}