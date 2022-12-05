using AuctionHouse.Data;
using AuctionHouse.DTOs;
using AuctionHouse.Models;
using System.Text.Json;

namespace AuctionHouse.Services.ItemService
{
    public class ItemRepository : IitemRepository
    {
        private readonly DataContext dataContext;
        public ItemRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public void Bid(Guid itemId, BidDTO bidDTO, Guid userId)
        {
            if (bidDTO is null)
            {
                throw new ArgumentNullException(nameof(bidDTO));
            }

            Item item = FindItemByGuid(itemId);
            User user = FindUserByGuid(userId);

            if (bidDTO.Bid > user.Money) 
            {
                throw new Exception("You don't have enough money.");
            }

            if (user.Money < item.Bid) 
            {
                throw new Exception("You don't have enough money.");
            }

            item.Bid = bidDTO.Bid;
            // Da dovursha

        }

        public Item GetItem(Guid id) // Da opravq tazi funkciq
        {
            Item item = dataContext.Items.Single(x => x.Id == id);
            return item;
        }

        public Item BuyNow(Guid id, User user)
        {
            Item item = dataContext.Items.Single(x => x.Id == id);
            
            if (item.IsAvailable == false) 
            {
                throw new Exception("This item is sold.");
            }
            
            if (user.Money < item.BuyPrice) 
            {
                throw new Exception("You don't have enough money.");
            }
            
            item.IsAvailable = false;
            item.BoughtFor = item.BuyPrice;
            item.EndBidDate = DateTime.UtcNow;
            item.BoughtUserId = user.Id;
            user.Money = user.Money - item.BuyPrice;
            dataContext.SaveChanges();
            return item;
        }

        public IEnumerable<Item> GetItems()
        {
            return dataContext.Items.ToList();
        }

        public void PostItem(ItemDTO itemDTO, Guid userId)
        {
            if (itemDTO is null)
            {
                throw new ArgumentNullException(nameof(itemDTO));
            }
            try
            {
                Item item = new Item()
                {
                    Name = itemDTO.Name,
                    Description = itemDTO.Description,
                    BuyPrice = itemDTO.BuyPrice,
                    StartingPrice = itemDTO.StartingPrice,
                    DateAdded = itemDTO.DateAdded,
                    StartingBidDate = itemDTO.StartingBidDate,
                    EndBidDate = itemDTO.EndBidDate,
                    Bid = itemDTO.StartingPrice,
                    AuthorUserId = userId
                };
                dataContext.Items.Add(item);
                dataContext.SaveChanges();
            } 
            catch (Exception)
            {
                throw new Exception("Invalid user.");
            }
        }

        public User FindUserByGuid(Guid userId)
        {
            User user = dataContext.Users.Where(user => user.Id == userId).Single();
            if (user is null)
            {
                throw new Exception("Invalid user id.");
            }
            return user;
        }

        public Item FindItemByGuid(Guid itemId)
        {
            Item item = dataContext.Items.Where(item => item.Id == itemId).Single();
            if (item is null) 
            {
                throw new Exception("Invalid item id.");
            }
            return item;
        }
    }
}
