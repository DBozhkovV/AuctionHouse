using AuctionHouse.Data;
using AuctionHouse.Models;

namespace AuctionHouse.Services.OrderService
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext dataContext;
        public OrderRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        
        public void DeleteOrder(Guid id, Guid userId)
        {
            Order orderToDelete = dataContext.Orders.Where(order => order.Id == id && order.UserId == userId).Single();
            dataContext.Orders.Remove(orderToDelete);
        }

        public Order GetOrder(Guid id, Guid userId)
        {
            return dataContext.Orders.Where(order => order.Id == id && order.UserId == userId)
                .Single();
        }

        public IEnumerable<Order> GetOrders(Guid userId)
        {
            return dataContext.Orders.Where(order => order.UserId == userId).ToList();
        }
    }
}
