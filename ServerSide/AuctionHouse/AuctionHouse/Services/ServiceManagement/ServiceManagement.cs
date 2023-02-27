using AuctionHouse.DAO.ItemDAO;
using AuctionHouse.Models;
using AuctionHouse.Services.EmailService;
using AuctionHouse.Services.ItemService;

namespace AuctionHouse.Services.ServiceManagement
{
    public class ServiceManagement : IServiceManagement
    {
        private readonly IItemRepository itemRepository;
        private readonly IItemService itemService;
        private readonly IEmailService emailService;

        public ServiceManagement(IItemRepository itemRepository, IEmailService emailService, IItemService itemService)
        {
            this.itemRepository = itemRepository;
            this.emailService = emailService;
            this.itemService = itemService;
        }

        public void CheckForExpiredAuctions()
        {
            List<Item> items = itemRepository.GetAllAvailableItems().ToList();
            items.ForEach(item =>
            {
                if (item.EndBidDate < DateTime.UtcNow)
                {
                    if (item.BidderId == null) 
                    {
                        itemService.DeleteItem(item);
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
                        User winner = itemRepository.GetUserByGuid((Guid)item.BidderId);
                        emailService.SendEmailToNotifyWon(winner.Email, item.Name, item.Bid);
                    }
                }
            });
        }
    }
}
