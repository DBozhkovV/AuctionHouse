using AuctionHouse.DTOs;
using AuctionHouse.Models;

namespace AuctionHouse.Services.ItemService
{
    public interface IitemRepository
    {
        IEnumerable<Item> GetItems();
        void PostItem(ItemDTO itemDTO, Guid userId);
        void Bid(Guid itemId, BidDTO bidDTO, Guid userId);
        Item BuyNow(Guid id, User user);
        User FindUserByGuid(Guid userId);
        Item FindItemByGuid(Guid itemId);
        Item GetItem(Guid id);
    }
}
