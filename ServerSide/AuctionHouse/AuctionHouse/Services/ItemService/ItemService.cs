﻿using AuctionHouse.DAO.ItemDAO;
using AuctionHouse.DTOs;
using AuctionHouse.Models;
using AuctionHouse.Services.AzureStorageService;
using AuctionHouse.Services.EmailService;

namespace AuctionHouse.Services.ItemService
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository itemRepository;
        private readonly IAzureStorageService azureStorageRepository;
        private readonly IEmailService emailService;

        public ItemService(IItemRepository itemRepository, IAzureStorageService azureStorageRepository, IEmailService emailService)
        {
            this.itemRepository = itemRepository;
            this.azureStorageRepository = azureStorageRepository;
            this.emailService = emailService;
        }

        public ItemResponse GetItem(Guid id)
        {
            Item item = FindItemByGuid(id);
            ItemResponse result = azureStorageRepository.ReturnItemResponse(item);
            return result;
        }

        public void Bid(Guid itemId, Guid userId, float money)
        {
            Item item = FindItemByGuid(itemId);
            User user = FindUserByGuid(userId);

            if (money > user.Balance)
            {
                throw new Exception("You don't have enough balance.");
            }

            if (user.Balance < item.Bid)
            {
                throw new Exception("You don't have enough balance.");
            }

            if (money < item.Bid)
            {
                throw new Exception("Your bid is too low.");
            }

            if (item.BidderId == null)
            {
                itemRepository.Bid(item, user, money);
            }
            else
            {
                User OutbiddedUser = itemRepository.GetUserByGuid((Guid)item.BidderId);
                itemRepository.ReturnMoneyToUser((Guid)item.BidderId, item.Bid); // return money to previous bidder
                itemRepository.Bid(item, user, money);
                emailService.SendEmailToNotifyOutbid(OutbiddedUser.Email, item.Name, money);
            }
        }

        public Item BuyNow(Guid id, User user)
        {
            Item item = itemRepository.GetItemById(id);

            if (item.IsAvailable == false)
            {
                throw new Exception("This item is sold.");
            }

            if (user.Balance < item.BuyPrice)
            {
                throw new Exception("You don't have enough money.");
            }

            Order order = new Order()
            {
                Item = item,
                User = user
            };

            if (item.BidderId != null)
            {
                Guid guid = (Guid)item.BidderId; // because I can't pass nullable to ReturnMoneyToUser function
                itemRepository.ReturnMoneyToUser(guid, item.Bid);
            }

            itemRepository.BuyItem(user, id, order);
            User userToAddBalance = itemRepository.GetUserByGuid(item.AuthorUserId);
            itemRepository.AddBalance(userToAddBalance, item.BuyPrice);
            return item;
        }

        public IEnumerable<ItemResponse> GetAvailableItems(int page) // return all available items
        {
            List<Item> availableItems = itemRepository.GetAvailableItems(page).ToList();
            IEnumerable<ItemResponse> itemResponses = azureStorageRepository.ReturnListOfItemResponses(availableItems);
            return itemResponses;
        }

        public IEnumerable<ItemResponse> GetItemsByCategory(Category category)
        {
            List<Item> availableItems = itemRepository.GetAvailableItemsByCategory(category).ToList();
            IEnumerable<ItemResponse> itemResponses = azureStorageRepository.ReturnListOfItemResponses(availableItems);
            return itemResponses;
        }

        public int GetAvailableItemsPagesCount()
        {
            double pages = itemRepository.GetAvailableItemsCount() / 5.0;
            return (int)Math.Ceiling(pages);
        }

        public ItemResponse GetNotAcceptedItem(Guid itemId)
        {
            Item item = FindItemByGuid(itemId);
            ItemResponse result = azureStorageRepository.ReturnItemResponse(item);
            return result;
        }
        
        public IEnumerable<ItemResponse> GetNotAcceptedItems()
        {
            List<Item> notAcceptedItems = itemRepository.GetNotAcceptedItems().ToList();
            IEnumerable<ItemResponse> itemResponses = azureStorageRepository.ReturnListOfItemResponses(notAcceptedItems);
            return itemResponses;
        }

        public IEnumerable<ItemResponse> SortItemsByHighToLow() // return all available items sorted by price from high to low(Descending)
        {
            List<Item> items = itemRepository.GetAllAvailableItems().OrderByDescending(item => item.BuyPrice).ToList();
            IEnumerable<ItemResponse> itemResponses = azureStorageRepository.ReturnListOfItemResponses(items);
            return itemResponses;
        }

        public IEnumerable<ItemResponse> SortItemsByLowToHigh() // return all available items sorted by low to high(Ascending)
        {
            List<Item> items = itemRepository.GetAllAvailableItems().OrderBy(item => item.BuyPrice).ToList();
            IEnumerable<ItemResponse> itemResponses = azureStorageRepository.ReturnListOfItemResponses(items);
            return itemResponses;
        }

        public IEnumerable<ItemResponse> SortItemsByNewest() // return all available items sorted by newest
        {
            List<Item> items = itemRepository.GetAllAvailableItems().OrderByDescending(item => item.DateAdded).ToList();
            IEnumerable<ItemResponse> itemResponses = azureStorageRepository.ReturnListOfItemResponses(items);
            return itemResponses;
        }

        public IEnumerable<ItemResponse> GetBidsByUserId(Guid userId)
        {
            List<Item> items = itemRepository.GetBidsByUserId(userId).ToList();
            IEnumerable<ItemResponse> itemResponses = azureStorageRepository.ReturnListOfItemResponses(items);
            return itemResponses;
        }

        public IEnumerable<ItemResponse> GetFiveNewestItems() 
        {
            List<Item> lastFiveItems = itemRepository.GetFiveNewestItems().ToList();
            IEnumerable<ItemResponse> itemResponses = azureStorageRepository.ReturnListOfItemResponses(lastFiveItems);
            return itemResponses;
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
                    Id = Guid.NewGuid(),
                    Name = itemDTO.Name,
                    Description = itemDTO.Description,
                    BuyPrice = itemDTO.BuyPrice,
                    StartingPrice = itemDTO.StartingPrice,
                    Bid = itemDTO.StartingPrice,
                    Category = itemDTO.Category,
                    DateAdded = itemDTO.DateAdded,
                    StartingBidDate = itemDTO.StartingBidDate,
                    EndBidDate = itemDTO.EndBidDate,
                    AuthorUserId = userId,
                    ImagesNames = new List<string>()
                };
                
                item.MainImageName = item.Id.ToString() + "_" + "main";
                azureStorageRepository.SaveImageAsync(itemDTO.MainImage, item.MainImageName);

                int count = 1;
                itemDTO.Images.ToList().ForEach(image =>
                {
                    string imageName = item.Id.ToString() + "_" + count.ToString();
                    azureStorageRepository.SaveImageAsync(image, imageName);
                    item.ImagesNames.Add(imageName);
                    count++;
                });

                itemRepository.InsertItem(item);
            }
            catch (Exception message)
            {
                throw new Exception(message.ToString());
            }
        }
        
        public User FindUserByGuid(Guid userId) // Guid is the id
        {
            return itemRepository.GetUserByGuid(userId);
        }

        public Item FindItemByGuid(Guid itemId) // Guid is the id
        {
            return itemRepository.GetItemById(itemId);
        }

        public void DeleteItem(Item item) 
        {
            azureStorageRepository.DeleteImage(item.MainImageName);
            item.ImagesNames.ForEach(imageName => azureStorageRepository.DeleteImage(imageName));
            itemRepository.DeleteItemById(item.Id);
        }

        public IEnumerable<ItemResponse> SearchItems(string search) // return items by containing keyword in there names
        {
            List<Item> items = itemRepository.GetSearchedItem(search).ToList();
            IEnumerable<ItemResponse> itemResponses = azureStorageRepository.ReturnListOfItemResponses(items);
            if (items is null)
            {
                throw new Exception("There is no item with this name.");
            }
            return itemResponses;
        }

        public void AcceptItem(Guid itemId) // When accept item, this item become available
        {
            itemRepository.AcceptItem(itemId);
        }

        public void RejectItem(Guid itemId) // When reject item, this item is deleted from database
        {
            itemRepository.RejectItem(itemId);
        }
    }
}
