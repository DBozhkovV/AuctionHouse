using AuctionHouse.DTOs;
using AuctionHouse.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuctionHouse.Services.AzureStorageService
{
    public interface IAzureStorageRepository
    {
        Task SaveImageAsync(IFormFile formFile, string blobName);

        Task<ItemResponse> GetImage(Item item);

        Task<List<Stream>> GetImages(string containerName, List<string> fileNames);
    }
}
