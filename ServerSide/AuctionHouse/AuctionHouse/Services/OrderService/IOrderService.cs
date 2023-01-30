using AuctionHouse.Models;

namespace AuctionHouse.Services.OrderService
{
    public interface IOrderService
    {
        Order GetOrderById(Guid id);

        IEnumerable<Order> GetOrdersByUser(Guid userId);

        void DeleteOrderById(Guid id);
    }
}
