using AuctionHouse.Data;
using AuctionHouse.DTOs;
using AuctionHouse.Models;
using AuctionHouse.Services.AzureStorageService;
using Microsoft.AspNetCore.Mvc;

namespace AuctionHouse.Services.ItemService
{
    public class ItemRepository : IitemRepository
    {
        private readonly DataContext dataContext;
        private readonly IAzureStorageRepository azureStorageRepository;

        public ItemRepository(DataContext dataContext, IAzureStorageRepository azureStorageRepository)
        {
            this.dataContext = dataContext;
            this.azureStorageRepository = azureStorageRepository;
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

        public IEnumerable<Item> GetAvailableItems() // return all available items
        {
            return dataContext.Items.Where(item => item.IsAvailable == true && item.IsAccepted == true).ToList();
        }

        public ItemResponse GetNotAcceptedItem(Guid itemId) 
        {
            Item item = FindItemByGuid(itemId);
            var itemResponse = new ItemResponse();
            
            return itemResponse;
        }

        public async Task PostItemAsync(ItemDTO itemDTO, Guid userId)
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
                    AuthorUserId = userId,
                    ImagesNames = new List<string>()
                };
                
                item.MainImageName = itemDTO.MainImage.FileName;
                azureStorageRepository.SaveImageAsync(itemDTO.MainImage, item.MainImageName);

                int count = 1;
                itemDTO.Images.ForEach(image =>
                {
                    azureStorageRepository.SaveImageAsync(image, image.FileName);
                    item.ImagesNames.Add(image.FileName);
                    count++;
                });
                dataContext.Items.Add(item);
                dataContext.SaveChanges();
            }
            catch (Exception message)
            {
                throw new Exception(message.ToString());
            }
        }
        
        public User FindUserByGuid(Guid userId) // Guid is the id
        {
            User user = dataContext.Users.Where(user => user.Id == userId).Single();
            if (user is null)
            {
                throw new Exception("Invalid user id.");
            }
            return user;
        }

        public Item FindItemByGuid(Guid itemId) // Guid is the id
        {
            Item item = dataContext.Items.Where(item => item.Id == itemId).Single();
            if (item is null)
            {
                throw new Exception("Invalid item id.");
            }
            return item;
        }

        public IEnumerable<Item> SearchItems(string search) // return items by containing keyword in there names
        {
            IEnumerable<Item> items = dataContext.Items.Where(item => item.Name.Contains(search)).ToList();
            if (items is null)
            {
                throw new Exception("There is no item with this name.");
            }
            return items;
        }

        public void AcceptItem(Guid itemId) // When accept item, this item become available
        {
            Item item = FindItemByGuid(itemId);
            item.IsAccepted = true;
            dataContext.SaveChanges();
        }

        public void RejectItem(Guid itemId) // When reject item, this item is deleted from database
        {
            Item item = FindItemByGuid(itemId);
            dataContext.Items.Remove(item);
            dataContext.SaveChanges();
        }
    }
}
