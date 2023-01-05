using AuctionHouse.DTOs;
using AuctionHouse.Models;

namespace AuctionHouse.Services.ItemService
{
    public interface IitemRepository
    {
        Item GetItem(Guid id);
        void Bid(Guid itemId, float Bid, Guid userId);
        Task<Item> BuyNowAsync(Guid id, User user);
        IEnumerable<Item> GetAvailableItems();
        IEnumerable<Item> GetNotAcceptedItems();
        Task PostItemAsync(ItemDTO itemDTO, Guid userId);
        User FindUserByGuid(Guid userId);
        Item FindItemByGuid(Guid itemId);
        IEnumerable<Item> SearchItems(string search);
        void AcceptItem(Guid itemId);
        Task SaveImageAsync(byte[] image, string blobName);
    }
}
