using System;

namespace CarService.Api.Services.AzureServices
{
    public class AzureBlobStorageSettings
    {
        public AzureBlobStorageSettings(string storageAccount,
                                       string storageKey)
        {
            if (string.IsNullOrEmpty(storageAccount))
                throw new ArgumentNullException("StorageAccount");

            if (string.IsNullOrEmpty(storageKey))
                throw new ArgumentNullException("StorageKey");

            this.StorageAccount = storageAccount;
            this.StorageKey = storageKey;
        }

        public string StorageAccount { get; }
        public string StorageKey { get; }
    }
}