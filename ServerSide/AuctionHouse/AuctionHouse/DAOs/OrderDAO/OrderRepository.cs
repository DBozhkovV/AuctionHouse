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

        public void DeleteOrder(Order order)
        {
            dataContext.Orders.Remove(order);
            dataContext.SaveChanges();
        }

        public void DeleteItem(Item item) 
        {
            dataContext.Items.Remove(item);
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
