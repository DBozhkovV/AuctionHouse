using AuctionHouse.Models;

namespace AuctionHouse.DAO.ItemDAO
{
    public interface IItemRepository
    {
        void AcceptItem(Guid id); // update the accepted item

        void BuyItem(User user, Guid itemId, Order order); // update bought item, user balance and add Order

        void DeleteItemById(Guid id);

        public void AddBalance(User user, float money);

        IEnumerable<Item> GetAllAvailableItems();

        IEnumerable<Item> GetAvailableItems(int page);

        int GetAvailableItemsCount();

        IEnumerable<Item> GetAvailableItemsByCategory(Category category);

        void ExpireItem(Item item, Order order); // update the expired item

        Item GetItemById(Guid id);

        void Bid(Item item, User user, float money);

        void ReturnMoneyToUser(Guid userId, float money); // return money to user if he is outbid

        IEnumerable<Item> GetNotAcceptedItems();

        IEnumerable<Item> GetBidsByUserId(Guid userId);

        IEnumerable<Item> GetSearchedItem(string search); // search is the keyword with which costumer search items

        User GetUserByGuid(Guid id);

        void InsertItem(Item item);

        void RejectItem(Guid id); // If item is reject then it is deleted from database
        
        IEnumerable<Item> GetFiveNewestItems();
    }
}
