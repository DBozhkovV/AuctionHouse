using AuctionHouse.DAO.ItemDAO;
using AuctionHouse.Models;

namespace AuctionHouse.Services.ServiceManagement
{
    public class ServiceManagement : IServiceManagement
    {
        private readonly IItemRepository itemRepository;

        public ServiceManagement(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        public void CheckForExpiredAuctions()
        {
            List<Item> items = itemRepository.GetAvailableItems().ToList();
            items.ForEach(item =>
            {
                if (item.EndBidDate < DateTime.UtcNow)
                {
                    if (item.BidderId == null) 
                    {
                        itemRepository.DeleteItemById(item.Id);
                    }
                    else
                    {
                        
                        Order newOrder = new Order
                        {
                            Id = Guid.NewGuid(),
                            ItemId = item.Id,
                            UserId = (Guid)item.BidderId
                        };
                        itemRepository.ExpireItem(item, newOrder);
                    }
                }
            });
        }
    }
}
