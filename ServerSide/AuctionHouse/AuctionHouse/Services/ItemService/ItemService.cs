﻿using AuctionHouse.DAO.ItemDAO;
using AuctionHouse.DTOs;
using AuctionHouse.Models;
using AuctionHouse.Services.AzureStorageService;

namespace AuctionHouse.Services.ItemService
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository itemRepository;
        private readonly IAzureStorageRepository azureStorageRepository;

        public ItemService(IItemRepository itemRepository, IAzureStorageRepository azureStorageRepository)
        {
            this.itemRepository = itemRepository;
            this.azureStorageRepository = azureStorageRepository;
        }

        public Task<ItemResponse> GetItem(Guid id)
        {
            Item item = FindItemByGuid(id);
            Task<ItemResponse> result = azureStorageRepository.ReturnItemResponse(item);
            return result;
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

        public IEnumerable<Task<ItemResponse>> GetAvailableItems() // return all available items
        {
            List<Item> availableItems = itemRepository.GetAvailableItems().ToList() ;
            IEnumerable<Task<ItemResponse>> itemResponses = azureStorageRepository.ReturnListOfItemResponses(availableItems);
            return itemResponses;
        }

        public IEnumerable<Task<ItemResponse>> GetItemsByCategory(Category category) 
        {
            List<Item> availableItems = itemRepository.GetAvailableItemsByCategory(category).ToList();
            IEnumerable<Task<ItemResponse>> itemResponses = azureStorageRepository.ReturnListOfItemResponses(availableItems);
            return itemResponses;
        }
        
        public IEnumerable<Task<ItemResponse>> GetNotAvailableItems()
        {
            List<Item> availableItems = itemRepository.GetNotAvailableItems().ToList();
            IEnumerable<Task<ItemResponse>> itemResponses = azureStorageRepository.ReturnListOfItemResponses(availableItems);
            return itemResponses;
        }

        public Task<ItemResponse> GetNotAcceptedItem(Guid itemId)
        {
            Item item = FindItemByGuid(itemId);
            Task<ItemResponse> result = azureStorageRepository.ReturnItemResponse(item);
            return result;
        }
        
        public IEnumerable<Task<ItemResponse>> GetNotAcceptedItems()
        {
            List<Item> notAcceptedItems = itemRepository.GetNotAcceptedItems().ToList();
            IEnumerable<Task<ItemResponse>> itemResponses = azureStorageRepository.ReturnListOfItemResponses(notAcceptedItems);
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

        public IEnumerable<Task<ItemResponse>> SearchItems(string search) // return items by containing keyword in there names
        {
            List<Item> items = itemRepository.GetSearchedItem(search).ToList();
            IEnumerable<Task<ItemResponse>> itemResponses = azureStorageRepository.ReturnListOfItemResponses(items);
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
