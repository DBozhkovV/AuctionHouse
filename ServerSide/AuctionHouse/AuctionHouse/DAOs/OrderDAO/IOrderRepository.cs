using AuctionHouse.Models;

namespace AuctionHouse.DAOs.OrderDAO
{
    public interface IOrderRepository
    {

        Order GetOrderById(Guid id);
        
        IEnumerable<Order> GetOrdersByUser(Guid userId);
        
        void DeleteOrderById(Guid id);
    }
}
