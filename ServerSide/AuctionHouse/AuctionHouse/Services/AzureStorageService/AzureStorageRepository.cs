using AuctionHouse.DTOs;
using AuctionHouse.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AuctionHouse.Services.AzureStorageService
{
    public class AzureStorageRepository : IAzureStorageRepository
    {
        private readonly IConfiguration configuration;
        private CloudStorageAccount storageAccount;

        public AzureStorageRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            storageAccount = CloudStorageAccount.Parse(this.configuration.GetConnectionString("AzureStorage"));
        }

        public async Task SaveImageAsync(IFormFile formFile, string blobName)
        {
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("itemimage");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);

            using (var stream = formFile.OpenReadStream())
            {
                byte[] imageBytes = new byte[formFile.Length];
                stream.Read(imageBytes, 0, (int)formFile.Length);
                await blockBlob.UploadFromByteArrayAsync(imageBytes, 0, (int)stream.Length);
                stream.Close();
            }
        }
        public async Task<ItemResponse> GetImage(Item item)
        {
            var blobClient = storageAccount.CreateCloudBlobClient();
            var result = new ItemResponse();
            result.Id = item.Id;
            result.Name = item.Name;
            result.Description = item.Description;
            result.BuyPrice = item.BuyPrice;
            result.StartingPrice = item.StartingPrice;
            result.Bid = item.Bid;
            result.DateAdded = item.DateAdded;
            result.StartingBidDate = item.StartingBidDate;
            result.EndBidDate = item.EndBidDate;
            
            result.Images = new List<byte[]>();
            // Get a reference to the container
            var container = blobClient.GetContainerReference("itemimage");

            // download the main image
            var blob = container.GetBlockBlobReference(item.MainImageName);
            var memoryStream = new MemoryStream();
            await blob.DownloadToStreamAsync(memoryStream);
            result.MainImage = memoryStream.ToArray();

            // download the list of images
            foreach (var imageName in item.ImagesNames)
            {
                var imageBlob = container.GetBlockBlobReference(imageName);
                var imageStream = new MemoryStream();
                await imageBlob.DownloadToStreamAsync(imageStream);
                result.Images.Add(imageStream.ToArray());
            }

            return result;
        }

        public async Task<List<Stream>> GetImages(string containerName, List<string> fileNames)
        {
            var blobClient = storageAccount.CreateCloudBlobClient();

            // Get a reference to the container
            var container = blobClient.GetContainerReference(containerName);

            var result = new List<Stream>();
            foreach (var fileName in fileNames)
            {
                // Get a reference to the blob
                var blob = container.GetBlockBlobReference(fileName);

                var memoryStream = new MemoryStream();
                await blob.DownloadToStreamAsync(memoryStream);
                memoryStream.Position = 0;
                result.Add(memoryStream);
            }
            return result;
        }

    }
}
