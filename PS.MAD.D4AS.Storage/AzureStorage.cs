using Microsoft.WindowsAzure.Storage.Auth;
using System;

namespace PS.MAD.D4AS.Storage
{
    public class AzureStorage : DataAccess.Contracts.IStorage
    {
        private readonly string storageAccount = "psmadd4asimagestorage";
        private readonly string storageKey = "5qgu7yiVOG5Ith7yROd4QTfIKjE+HFRbx2yCkjxX9gJcJLgT8EwQnSp3wr8gQ3wKUFo0p1Fzbv6fRDBb8gs34w==";

        public async void StoreImage(Entities.Image image)
        {
            var storageUrl = System.IO.Path.Combine("https://psmadd4asimagestorage.blob.core.windows.net/images/", $"{image.Id.ToString()}-{image.Name}");
            
            var credentials = new StorageCredentials(storageAccount, storageKey);
            var blob = new Microsoft.WindowsAzure.Storage.Blob.CloudBlockBlob(new Uri(storageUrl), credentials);
            await blob.UploadFromByteArrayAsync(image.Body, 0, image.Body.Length);
        }

        public async void StoreVideo(Entities.Video video)
        {
            var storageUrl = System.IO.Path.Combine("https://psmadd4asimagestorage.blob.core.windows.net/videos/", $"{video.Id.ToString()}-{video.Name}");

            var credentials = new StorageCredentials(storageAccount, storageKey);
            var blob = new Microsoft.WindowsAzure.Storage.Blob.CloudBlockBlob(new Uri(storageUrl), credentials);
            await blob.UploadFromByteArrayAsync(video.Body, 0, video.Body.Length);
        }
    }
}
