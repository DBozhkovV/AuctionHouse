using System.ComponentModel.DataAnnotations;

namespace AuctionHouse.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public DateTime dateOrdered { get; set; }
        public bool IsOrderActive { get; set; } = true;

        public Guid UserId { get; set; }
        // Da pitam nachalnika
    }
}
