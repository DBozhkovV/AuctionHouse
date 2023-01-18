using AuctionHouse.DAO.ItemRepositoryies;
using AuctionHouse.DTOs;
using AuctionHouse.Models;
using AuctionHouse.Services.AzureStorageService;

namespace AuctionHouse.Services.ItemService
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IAzureStorageRepository azureStorageRepository;

        public ItemService(IItemRepository _itemRepository, IAzureStorageRepository azureStorageRepository)
        {
            this._itemRepository = _itemRepository;
            this.azureStorageRepository = azureStorageRepository;
        }

        public Item GetItem(Guid id)
        {
            return _itemRepository.GetItemById(id);
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
            Item item = _itemRepository.GetItemById(id);

            if (item.IsAvailable == false)
            {
                throw new Exception("This item is sold.");
            }

            if (user.Balance < item.BuyPrice)
            {
                throw new Exception("You don't have enough money.");
            }

            _itemRepository.BuyItem(id);

            user.Balance = user.Balance - item.BuyPrice; // da go promenq v user repoto

            Order order = new Order() // da izmislq kude da go pravq tva
            {
                DateOrdered = DateTime.UtcNow,
                Item = item,
                User = user
            };
            
            return item;
        }

        public IEnumerable<Task<ItemResponse>> GetAvailableItems() // return all available items
        {
            List<Item> availableItems = _itemRepository.GetAvailableItems().ToList() ;
            IEnumerable<Task<ItemResponse>> itemResponses = azureStorageRepository.ReturnListOfItemResponses(availableItems);
            return itemResponses;
        }

        public IEnumerable<Task<ItemResponse>> GetNotAvailableItems() // da dovursha
        {
            List<Item> availableItems = _itemRepository.GetNotAvailableItems().ToList();
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
            List<Item> notAcceptedItems = _itemRepository.GetNotAcceptedItems().ToList();
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
                
                _itemRepository.InsertItem(item);
            }
            catch (Exception message)
            {
                throw new Exception(message.ToString());
            }
        }
        
        public User FindUserByGuid(Guid userId) // Guid is the id
        {
            return _itemRepository.GetUserByGuid(userId);
        }

        public Item FindItemByGuid(Guid itemId) // Guid is the id
        {
            return _itemRepository.GetItemById(itemId);
        }

        public IEnumerable<Item> SearchItems(string search) // return items by containing keyword in there names
        {
            IEnumerable<Item> items = _itemRepository.GetSearchedItem(search);
            if (items is null)
            {
                throw new Exception("There is no item with this name.");
            }
            return items;
        }

        public void AcceptItem(Guid itemId) // When accept item, this item become available
        {
            _itemRepository.AcceptItem(itemId);
        }

        public void RejectItem(Guid itemId) // When reject item, this item is deleted from database
        {
            _itemRepository.RejectItem(itemId);
        }
    }
}
