using AuctionHouse.DAOs.OrderDAO;
using AuctionHouse.Models;

namespace AuctionHouse.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void DeleteOrderById(Guid id)
        {
            _orderRepository.DeleteOrderById(id);
        }

        public Order GetOrderById(Guid id)
        {
            return _orderRepository.GetOrderById(id);
        }

        public IEnumerable<Order> GetOrdersByUser(Guid userId)
        {
            return _orderRepository.GetOrdersByUser(userId);
        }
    }
}
