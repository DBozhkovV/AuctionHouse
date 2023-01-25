using AuctionHouse.DTOs;
using AuctionHouse.Models;

namespace AuctionHouse.Services.ItemService
{
    public interface IItemService
    {
        Task<ItemResponse> GetItem(Guid id);
        
        void Bid(Guid itemId, float Bid, Guid userId);

        Item BuyNow(Guid id, User user);

        IEnumerable<Task<ItemResponse>> GetAvailableItems();

        IEnumerable<Task<ItemResponse>> GetNotAvailableItems();

        IEnumerable<Task<ItemResponse>> GetNotAcceptedItems();

        Task<ItemResponse> GetNotAcceptedItem(Guid itemId);

        void PostItem(ItemDTO itemDTO, Guid userId);

        User FindUserByGuid(Guid userId);
        
        Item FindItemByGuid(Guid itemId);

        IEnumerable<Task<ItemResponse>> SearchItems(string search);

        void AcceptItem(Guid itemId);

        void RejectItem(Guid itemId);
    }
}
