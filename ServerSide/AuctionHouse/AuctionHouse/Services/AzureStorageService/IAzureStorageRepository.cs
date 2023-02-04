using AuctionHouse.DTOs;
using AuctionHouse.Models;

namespace AuctionHouse.Services.AzureStorageService
{
    public interface IAzureStorageRepository
    {
        Task SaveImageAsync(IFormFile formFile, string blobName);

        Task<ItemResponse> ReturnItemResponse(Item item);

        IEnumerable<Task<ItemResponse>> ReturnListOfItemResponses(List<Item> items);

        void DeleteImage(string blobName);

        string GetImageType(string fileName);
    }
}
