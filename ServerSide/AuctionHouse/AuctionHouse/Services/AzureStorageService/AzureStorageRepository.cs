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
        public async Task<ItemResponse> ReturnItemResponse(Item item)
        {
            var blobClient = storageAccount.CreateCloudBlobClient();
            var result = new ItemResponse
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                BuyPrice = item.BuyPrice,
                StartingPrice = item.StartingPrice,
                Bid = item.Bid,
                DateAdded = item.DateAdded,
                StartingBidDate = item.StartingBidDate,
                EndBidDate = item.EndBidDate,
                MainImage = new ImageDTO(),
                Images = new List<ImageDTO>()
            };
            
            // Get a reference to the container
            var container = blobClient.GetContainerReference("itemimage");

            // download the main image
            var blob = container.GetBlockBlobReference(item.MainImageName);
            var memoryStream = new MemoryStream();
            await blob.DownloadToStreamAsync(memoryStream);
            result.MainImage.Image = memoryStream.ToArray();
            result.MainImage.ImageType = GetImageType(item.MainImageName);
            
            // download the list of images
            foreach (var imageName in item.ImagesNames)
            {
                var imageBlob = container.GetBlockBlobReference(imageName);
                var imageStream = new MemoryStream();
                await imageBlob.DownloadToStreamAsync(imageStream);
                ImageDTO image = new ImageDTO
                {
                    Image = imageStream.ToArray(),
                    ImageType = GetImageType(imageName)
                };
                result.Images.Add(image);
            }

            return result;
        }

        public IEnumerable<Task<ItemResponse>> ReturnListOfItemResponses(List<Item> items)
        {
            List<Task<ItemResponse>> itemResponses = new List<Task<ItemResponse>>();
            items.ForEach(item =>
            {
                itemResponses.Add(ReturnItemResponse(item));
            });
            return itemResponses;
        }

        public string GetImageType(string fileName)
        {
            if (fileName.Contains(".jpg")) {
                return "image/jpg";
            }
            else if (fileName.Contains(".png"))
            {
                return "image/png";
            }
            else 
            {
                return "image/jpeg";
            }
        }
    }
}
