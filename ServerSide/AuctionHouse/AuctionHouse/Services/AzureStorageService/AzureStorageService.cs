using AuctionHouse.DTOs;
using AuctionHouse.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AuctionHouse.Services.AzureStorageService
{
    public class AzureStorageService : IAzureStorageService
    {
        private readonly IConfiguration configuration;
        private const string ItemsImagesURL = "https://auctionhouseimagestorage.blob.core.windows.net/itemimage/";
        private CloudStorageAccount storageAccount;

        public AzureStorageService(IConfiguration configuration)
        {
            this.configuration = configuration;
            storageAccount = CloudStorageAccount.Parse(this.configuration.GetConnectionString("AzureStorage"));
        }

        public async Task SaveImageAsync(IFormFile formFile, string blobName)
        {
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("itemimage"); // return a reference to the container
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName); // Create a blob with the name of the file

            using (var stream = formFile.OpenReadStream()) 
            {
                byte[] imageBytes = new byte[formFile.Length]; // Create byte array
                stream.Read(imageBytes, 0, (int)formFile.Length); // reads into imageBytes
                await blockBlob.UploadFromByteArrayAsync(imageBytes, 0, (int)stream.Length);
                stream.Close();
            }
        }
        
        public ItemResponse ReturnItemResponse(Item item)
        {
            var result = new ItemResponse
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                BuyPrice = item.BuyPrice,
                StartingPrice = item.StartingPrice,
                BoughtFor = item.BoughtFor,
                IsAccepted = item.IsAccepted,
                IsAvailable = item.IsAvailable,
                Bid = item.Bid,
                DateAdded = item.DateAdded,
                StartingBidDate = item.StartingBidDate,
                EndBidDate = item.EndBidDate,
                Images = new List<string>()
            };

            result.MainImage = ItemsImagesURL + item.MainImageName;
            
            foreach (var imageName in item.ImagesNames)
            {
                string image = ItemsImagesURL + imageName;
                result.Images.Add(image);
            }
   
            return result;
        }

        public IEnumerable<ItemResponse>ReturnListOfItemResponses(List<Item> items)
        {
            List<ItemResponse> itemResponses = new List<ItemResponse>();
            items.ForEach(item =>
            {
                itemResponses.Add(ReturnItemResponse(item));
            });
            return itemResponses;
        }

        public void DeleteImage(string blobName) 
        {
            var blobClient = storageAccount.CreateCloudBlobClient();

            // Get a reference to the container
            var container = blobClient.GetContainerReference("itemimage");
            
            var blob = container.GetBlockBlobReference(blobName);
            blob.DeleteAsync();
        }
    }
}
