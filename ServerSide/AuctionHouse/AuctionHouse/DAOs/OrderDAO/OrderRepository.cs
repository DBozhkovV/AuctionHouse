using AuctionHouse.Data;
using AuctionHouse.Models;

namespace AuctionHouse.DAOs.OrderDAO
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext dataContext;

        public OrderRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public void DeleteOrderById(Guid id)
        {
            Order orderToDelete = dataContext.Orders.Find(id);
            if (orderToDelete == null)
            {
                throw new Exception("Order not found");
            }
            dataContext.Orders.Remove(orderToDelete);
            dataContext.SaveChanges();
        }

        public Order GetOrderById(Guid id)
        {
            Order order = dataContext.Orders.Find(id);
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            return order;
        }

        public IEnumerable<Order> GetOrdersByUser(Guid userId)
        {
            return dataContext.Orders.Where(order => order.UserId == userId).ToList();
        }
    }
}
