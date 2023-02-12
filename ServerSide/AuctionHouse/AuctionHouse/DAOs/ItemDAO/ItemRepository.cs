using AuctionHouse.Data;
using AuctionHouse.Models;

namespace AuctionHouse.DAO.ItemDAO
{
    public class ItemRepository : IItemRepository
    {
        private readonly DataContext dataContext;
        
        public ItemRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public void AcceptItem(Guid id)
        {
            dataContext.Items
                .Where(item => item.Id == id)
                .ToList()
                .ForEach(item => {
                    item.IsAccepted = true;
                });
            dataContext.SaveChanges();
        }

        public void BuyItem(User user, Guid itemId, Order order)
        {
            Item item = GetItemById(itemId);
            item.IsAvailable = false;
            item.BoughtFor = item.BuyPrice;
            item.EndBidDate = DateTime.UtcNow;
            user.Balance -= item.BuyPrice;
            item.BidderId = null;
            item.Bid = item.StartingPrice;
            dataContext.Orders.Add(order);
            dataContext.SaveChanges();
        }

        public void DeleteItemById(Guid id)
        {
            Item item = GetItemById(id);
            dataContext.Items.Remove(item);
            dataContext.SaveChanges();
        }

        public void AddBalance(User user, float money) 
        {
            user.Balance += money;
            dataContext.SaveChanges();
        }

        public void ReturnMoneyToUser(Guid userId, float money)
        {
            User user = GetUserByGuid(userId);
            user.Balance += money;
            dataContext.SaveChanges();
        }
        
        public IEnumerable<Item> GetAvailableItems()
        {
            return dataContext.Items
                .Where(item => item.IsAvailable == true && item.IsAccepted == true)
                .ToList();
        }

        public IEnumerable<Item> GetAvailableItemsByCategory(Category category) 
        {
            return dataContext.Items
                .Where(item => item.IsAvailable == true && item.IsAccepted == true && item.Category == category)
                .ToList();
        }

        public void ExpireItem(Item item, Order order) 
        {
            item.IsAvailable = false;
            item.BoughtFor = item.Bid;
            User author = GetUserByGuid(item.AuthorUserId);
            author.Balance += item.Bid;
            //User boughter = GetUserByGuid((Guid)item.BidderId);
            //boughter.Balance -= item.Bid;
            dataContext.Orders.Add(order);
            item.BidderId = null;
            dataContext.SaveChanges();
        }

        public Item GetItemById(Guid id)
        {
            return dataContext.Items.Single(item => item.Id.Equals(id));
        }

        public void Bid(Item item, User user, float money)  // proverka dali usera ima tolkova pari
        {
            user.Balance -= money;
            item.BidderId = user.Id;
            item.Bid = money;
            dataContext.SaveChanges();
        }
            
        public Item GetNotAcceptedItemById(Guid id)
        {
            return dataContext.Items.Single(item => item.Id.Equals(id));
        }

        public IEnumerable<Item> GetBidsByUserId(Guid userId) 
        {
            return dataContext.Items
                .Where(item => item.BidderId == userId)
                .ToList();
        }

        public IEnumerable<Item> GetNotAcceptedItems()
        {
            /*
            List<Item> items = new List<Item>();
            _dataContext.Items.ToList().ForEach(item => {
                if (item.IsAccepted == false) {
                    items.Add(item);
                }
            });
            return items;
            */
            
            return dataContext.Items
                .Where(item => item.IsAvailable == true && item.IsAccepted == false)
                .ToList();
        }

        public IEnumerable<Item> GetNotAvailableItems()
        {
            return dataContext.Items
                .Where(item => item.IsAvailable == false && item.IsAccepted == true)
                .ToList();
        }

        public IEnumerable<Item> GetSearchedItem(string search)
        {
            return dataContext.Items
                .Where(item => item.Name.Contains(search) && item.IsAccepted == true && item.IsAvailable == true)
                .ToList();
        }

        public User GetUserByGuid(Guid id)
        {
            return dataContext.Users.Single(user => user.Id.Equals(id));
        }

        public void InsertItem(Item item)
        {
            dataContext.Items.Add(item);
            dataContext.SaveChanges();
        }

        public void RejectItem(Guid id)
        {
            Item item = GetItemById(id);
            dataContext.Items.Remove(item);
            dataContext.SaveChanges();
        }

        public IEnumerable<Item> GetFiveNewestItems()
        {
            return dataContext.Items
                .Where(item => item.IsAvailable == true && item.IsAccepted == true)
                .OrderByDescending(item => item.DateAdded) // From newest to oldest
                .Take(5)
                .ToList();
        }
    }
}
