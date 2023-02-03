using AuctionHouse.DAOs.OrderDAO;
using AuctionHouse.DTOs;
using AuctionHouse.Models;
using AuctionHouse.Services.AzureStorageService;

namespace AuctionHouse.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IAzureStorageRepository azureStorageRepository;

        public OrderService(IOrderRepository orderRepository, IAzureStorageRepository azureStorageRepository = null)
        {
            this.orderRepository = orderRepository;
            this.azureStorageRepository = azureStorageRepository;
        }

        public void DeleteOrderById(Guid id)
        {
            orderRepository.DeleteOrderById(id);
        }

        public OrderDTO GetOrderById(Guid id)
        {
            Order order = orderRepository.GetOrderById(id);
            Item item = orderRepository.getItemById(order.ItemId);

            Task<ItemResponse> itemResponse = azureStorageRepository.ReturnItemResponse(item);

            OrderDTO orderDTO = new OrderDTO
            {
                Id = order.Id,
                DateOrdered = order.DateOrdered,
                IsOrderActive = order.IsOrderActive,
                IsOrderCompleted = order.IsOrderCompleted,
                ItemResponse = itemResponse
            };

            return orderDTO;
        }

        public IEnumerable<Order> GetOrdersByUser(Guid userId)
        {
            return orderRepository.GetOrdersByUser(userId);
        }
    }
}
