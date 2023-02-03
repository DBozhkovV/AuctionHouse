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
            Order orderToDelete = dataContext.Orders.Where(order => order.Id.Equals(id)).SingleOrDefault();
            if (orderToDelete == null)
            {
                throw new Exception("Order not found.");
            }
            dataContext.Orders.Remove(orderToDelete);
            dataContext.SaveChanges();
        }

        public Item getItemById(Guid id)
        {
            return dataContext.Items
                .Single(item => item.Id.Equals(id));
        }

        public Order GetOrderById(Guid id)
        {
            return dataContext.Orders
                .Single(order => order.Id.Equals(id));
        }

        public IEnumerable<Order> GetOrdersByUser(Guid userId)
        {
            return dataContext.Orders.Where(order => order.UserId == userId).ToList();
        }
    }
}
