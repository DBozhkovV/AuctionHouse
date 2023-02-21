using AuctionHouse.DTOs;
using AuctionHouse.Models;

namespace AuctionHouse.Services.AzureStorageService
{
    public interface IAzureStorageService
    {
        Task SaveImageAsync(IFormFile formFile, string blobName);

        ItemResponse ReturnItemResponse(Item item);

        IEnumerable<ItemResponse> ReturnListOfItemResponses(List<Item> items);

        void DeleteImage(string blobName);
    }
}
