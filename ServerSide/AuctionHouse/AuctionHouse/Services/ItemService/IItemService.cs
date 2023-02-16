using AuctionHouse.DTOs;
using AuctionHouse.Models;

namespace AuctionHouse.Services.ItemService
{
    public interface IItemService
    {
        Task<ItemResponse> GetItem(Guid id);

        void Bid(Guid itemId, Guid userId, float money);

        Item BuyNow(Guid id, User user);

        IEnumerable<Task<ItemResponse>> GetAvailableItems(int page);

        IEnumerable<Task<ItemResponse>> GetItemsByCategory(Category category);

        int GetAvailableItemsPagesCount();

        IEnumerable<Task<ItemResponse>> GetNotAcceptedItems();

        IEnumerable<Task<ItemResponse>> SortItemsByHighToLow(); // sort items by price from high to low

        IEnumerable<Task<ItemResponse>> SortItemsByLowToHigh(); // sort items by price from low to high

        IEnumerable<Task<ItemResponse>> SortItemsByNewest(); // sort items by newest

        IEnumerable<Task<ItemResponse>> GetBidsByUserId(Guid userId);

        IEnumerable<Task<ItemResponse>> GetFiveNewestItems();

        Task<ItemResponse> GetNotAcceptedItem(Guid itemId);

        void PostItem(ItemDTO itemDTO, Guid userId);

        User FindUserByGuid(Guid userId);
        
        Item FindItemByGuid(Guid itemId);

        IEnumerable<Task<ItemResponse>> SearchItems(string search);

        void AcceptItem(Guid itemId);

        void RejectItem(Guid itemId);
    }
}
