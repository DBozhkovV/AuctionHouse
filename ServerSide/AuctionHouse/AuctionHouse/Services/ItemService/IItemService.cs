using AuctionHouse.DTOs;
using AuctionHouse.Models;

namespace AuctionHouse.Services.ItemService
{
    public interface IItemService
    {
        ItemResponse GetItem(Guid id);

        void Bid(Guid itemId, Guid userId, float money);

        Item BuyNow(Guid id, User user);

        IEnumerable<ItemResponse> GetAvailableItems(int page);

        IEnumerable<ItemResponse> GetItemsByCategory(Category category);

        int GetAvailableItemsPagesCount();

        IEnumerable<ItemResponse> GetNotAcceptedItems();

        IEnumerable<ItemResponse> SortItemsByHighToLow(); // sort items by price from high to low

        IEnumerable<ItemResponse> SortItemsByLowToHigh(); // sort items by price from low to high

        IEnumerable<ItemResponse> SortItemsByNewest(); // sort items by newest

        IEnumerable<ItemResponse> GetBidsByUserId(Guid userId);

        IEnumerable<ItemResponse> GetFiveNewestItems();

        ItemResponse GetNotAcceptedItem(Guid itemId);

        void PostItem(ItemDTO itemDTO, Guid userId);

        User FindUserByGuid(Guid userId);
        
        Item FindItemByGuid(Guid itemId);

        IEnumerable<ItemResponse> SearchItems(string search);

        void AcceptItem(Guid itemId);

        void RejectItem(Guid itemId);
    }
}
