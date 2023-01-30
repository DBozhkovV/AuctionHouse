using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionHouse.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime DateOrdered { get; set; } = DateTime.UtcNow;

        public bool IsOrderActive { get; set; } = true;

        public bool IsOrderCompleted { get; set; } = false;

        public Guid ItemId { get; set; }

        [ForeignKey("ItemId")]
        public Item Item { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
 
        public Order()
        {
        }
    }
}
