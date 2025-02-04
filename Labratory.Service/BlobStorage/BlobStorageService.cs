using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Labratory.Domain.Configs;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Labratory.Service.BlobStorage
{
    public class BlobStorageService : IBlobStorageService
    {

        private readonly BlobContainerConfig config;

        public BlobStorageService(IOptions<BlobContainerConfig> options)
        {
            this.config = options.Value;
        }

        public async Task<string> SaveImageIntoBlobStorage(string extention, string newName, Stream stream)
        {
            var service = new BlobContainerClient(this.config.Url, this.config.ContainerName);
            string completeName = $"{newName}{extention}";
            BlobClient blobClient = service.GetBlobClient(completeName);

            var mimeTypeDefault = this.config.CommonMIMETypes.FirstOrDefault(x => x.Key == "default");
            var mimeType = this.config.CommonMIMETypes.FirstOrDefault(x => x.Key.Contains(extention.Replace(".", "")));

            //var result = await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = "text/plain" });
            var result = await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = (mimeType.Value ?? mimeTypeDefault.Value) });

            return blobClient.Uri.AbsoluteUri;
        }

        public async Task DeleteImageIntoBlobStorage(string completeName)
        {
            var service = new BlobContainerClient(this.config.Url, this.config.ContainerName);
            BlobClient blobClient = service.GetBlobClient(completeName);

            // var result = await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = (mimeType.Value ?? mimeTypeDefault.Value) });
            var result = await blobClient.DeleteIfExistsAsync();
        }

    }
}
