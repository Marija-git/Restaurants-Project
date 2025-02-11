using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Interfaces;
using Restaurants.Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Storage
{
    internal class BlobStorageService(IOptions<BlobStorageSettings> blobStorageSettingsOptions) : IBlobStorageService
    {
        private readonly BlobStorageSettings _blobStorageSettings = blobStorageSettingsOptions.Value;
        public async Task<string> UploadToBlobStorage(Stream data, string fileName)
        {
            //konekcija koda sa azure storage acc / sa blob service
            var blobServiceClient = new BlobServiceClient(_blobStorageSettings.ConnectionString);

            //pristup container-u
            var containerClient = blobServiceClient.GetBlobContainerClient(_blobStorageSettings.LogosContainerName);

            //to upload file -> create blob client object
            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(data);
            var blobUrl = blobClient.Uri.ToString();

            return blobUrl;
        }
    }
}
