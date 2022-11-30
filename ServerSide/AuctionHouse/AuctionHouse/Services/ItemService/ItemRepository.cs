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

        public void Bid(BidDTO bidDTO)
        {
            if (bidDTO is null)
            {
                throw new ArgumentNullException(nameof(bidDTO));
            }

            User BidUser = new User(); // da pitam 
            foreach (User user in dataContext.Users)
            {
                if (user.Id == bidDTO.BidUser)
                {
                    BidUser = user;
                }
            }

            Item BidItem = new Item(); // da pitam 
            foreach (Item item in dataContext.Items)
            {
                if (item.Id == bidDTO.ItemId)
                {
                    BidItem = item;
                }
            }

            if (BidUser.Money >= BidItem.Bid)
            {
                BidUser.Money -= BidItem.Bid;
                BidItem.Bid = bidDTO.Bid;
                dataContext.SaveChanges();
            }
        }

        public void BuyNow(int id)
        {
            foreach (Item item in dataContext.Items)
            {
                if (item.Id == id)
                {
                    item.IsAvailable = false;
                    item.BoughtFor = item.BuyPrice;
                    item.EndBidDate = DateTime.Now;
                    dataContext.SaveChanges();
                    // parite na usera
                    return;
                }
            }
            // Trqbva da se vzemem sesiqta na usera
            throw new Exception("Can't find item with given id.");
        }

        public IEnumerable<Item> GetItems()
        {
            return dataContext.Items;
        }

        public void PostItem(ItemDTO itemDTO)
        {
            if (itemDTO is null)
            {
                throw new ArgumentNullException(nameof(itemDTO));
            }

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
                UserId = itemDTO.UserId
            };
            dataContext.Items.Add(item);
            dataContext.SaveChanges();
        }
    }
}
