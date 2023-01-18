using AuctionHouse.Data;
using AuctionHouse.Models;

namespace AuctionHouse.DAO.ItemRepositoryies
{
    public class ItemRepository : IItemRepository
    {
        private readonly DataContext _dataContext;
        
        public ItemRepository(DataContext _dataContext)
        {
            this._dataContext = _dataContext;
        }

        public void AcceptItem(Guid id)
        {
            Item item = GetItemById(id);
            item.IsAccepted = true;
            _dataContext.SaveChanges();
        }

        public void BuyItem(Guid itemId)
        {
            Item item = GetItemById(itemId);
            item.IsAvailable = false;
            item.BoughtFor = item.BuyPrice;
            item.EndBidDate = DateTime.UtcNow;  
            _dataContext.SaveChanges();
        }

        public void DeleteItemById(Guid id)
        {
            Item item = GetItemById(id);
            _dataContext.Items.Remove(item);
            _dataContext.SaveChanges();
        }

        public IEnumerable<Item> GetAvailableItems()
        {
            return _dataContext.Items
                .Where(item => item.IsAvailable == true && item.IsAccepted == true)
                .ToList();
        }

        public Item GetItemById(Guid id)
        {
            return _dataContext.Items.Single(item => item.Id.Equals(id));
        }

        public Item GetNotAcceptedItemById(Guid id)
        {
            return _dataContext.Items.Single(item => item.Id.Equals(id));
        }

        public IEnumerable<Item> GetNotAcceptedItems()
        {
            List<Item> items = new List<Item>();
            _dataContext.Items.ToList().ForEach(item => {
                if (item.IsAccepted == false) {
                    items.Add(item);
                }
            });
            return items;

            /*
            return _dataContext.Items
                .Where(item => item.IsAvailable == true && item.IsAccepted == false)
                .ToList();
            */
        }

        public IEnumerable<Item> GetNotAvailableItems()
        {
            return _dataContext.Items
                .Where(item => item.IsAvailable == false && item.IsAccepted == true)
                .ToList();
        }

        public IEnumerable<Item> GetSearchedItem(string search)
        {
            return _dataContext.Items.Where(item => item.Name.Contains(search)).ToList();
        }

        public User GetUserByGuid(Guid id)
        {
            return _dataContext.Users.Single(user => user.Id.Equals(id));
        }

        public void InsertItem(Item item)
        {
            _dataContext.Items.Add(item);
            _dataContext.SaveChanges();
        }

        public void RejectItem(Guid id)
        {
            Item item = GetItemById(id);
            _dataContext.Items.Remove(item);
            _dataContext.SaveChanges();
        }
    }
}
