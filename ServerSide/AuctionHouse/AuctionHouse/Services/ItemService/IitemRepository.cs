using AuctionHouse.DTOs;
using AuctionHouse.Models;

namespace AuctionHouse.Services.ItemService
{
    public interface IitemRepository
    {
        IEnumerable<Item> GetItems();
        void PostItem(ItemDTO itemDTO);
        void Bid(BidDTO bidDTO);
        void BuyNow(int id);
    }
}
