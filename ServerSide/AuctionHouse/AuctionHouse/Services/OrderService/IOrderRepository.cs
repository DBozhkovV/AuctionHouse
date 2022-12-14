using AuctionHouse.Models;

namespace AuctionHouse.Services.OrderService
{
    public interface IOrderRepository
    {
        Order GetOrder(Guid id, Guid userId);
        IEnumerable<Order> GetOrders(Guid userId);
        void DeleteOrder(Guid id, Guid userId);
    }
}
