using AuctionHouse.Data;
using AuctionHouse.DTOs;
using AuctionHouse.Models;

namespace AuctionHouse.Services.ItemService
{
    public class ItemRepository : IitemRepository
    {
        private readonly DataContext dataContext;
        public ItemRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public Item GetItem(Guid id)
        {
            Item item = dataContext.Items.Single(x => x.Id == id);
            return item;
        }

        public void Bid(Guid itemId, float Bid, Guid userId)
        {
            Item item = FindItemByGuid(itemId);
            User user = FindUserByGuid(userId);

            if (Bid > user.Balance)
            {
                throw new Exception("You don't have enough balance.");
            }

            if (user.Balance < item.Bid)
            {
                throw new Exception("You don't have enough balance.");
            }

            item.Bid = Bid;
            // Da dovursha

        }

        public async Task<Item> BuyNowAsync(Guid id, User user)
        {
            Item item = dataContext.Items.Single(x => x.Id == id);

            if (item.IsAvailable == false)
            {
                throw new Exception("This item is sold.");
            }

            if (user.Balance < item.BuyPrice)
            {
                throw new Exception("You don't have enough money.");
            }

            item.IsAvailable = false;
            item.BoughtFor = item.BuyPrice;
            item.EndBidDate = DateTime.UtcNow;
            user.Balance = user.Balance - item.BuyPrice;

            Order order = new Order()
            {
                DateOrdered = DateTime.UtcNow,
                Item = item,
                User = user
            };

            dataContext.Orders.Add(order);
            await dataContext.SaveChangesAsync();
            return item;
        }

        public IEnumerable<Item> GetAvailableItems()
        {
            return dataContext.Items.Where(item => item.IsAvailable == true && item.IsAccepted == true).ToList();
        }

        public IEnumerable<Item> GetNotAcceptedItems()
        {
            return dataContext.Items.Where(item => item.IsAccepted == false).ToList();
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
                    Bid = itemDTO.StartingPrice,
                    DateAdded = itemDTO.DateAdded,
                    StartingBidDate = itemDTO.StartingBidDate,
                    EndBidDate = itemDTO.EndBidDate,
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

        public IEnumerable<Item> SearchItems(string search)
        {
            IEnumerable<Item> items = dataContext.Items.Where(item => item.Name.Contains(search)).ToList();
            if (items is null)
            {
                throw new Exception("There is no item with this name.");
            }

            return items;
        }

        public void AcceptItem(Guid itemId) 
        {
            Item item = FindItemByGuid(itemId);
            item.IsAccepted = true;
            dataContext.SaveChanges();
        }
    }
}
