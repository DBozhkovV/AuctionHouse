using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionHouse.Models
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // A new key will be generated when a user is added
        public int Id { get; set; }
        [Required] public string Name { get; set; } = null!;
        [Required] public string Description { get; set; } = null!;
        [Required] public float BuyPrice { get; set; }
        [Required] public float StartingPrice { get; set; }
        public float Bid { get; set; } = 0;
        [Required] public DateTime DateAdded { get; set; }
        [Required] public DateTime StartingBidDate { get; set; }
        [Required] public DateTime EndBidDate { get; set; }
        [ForeignKey("UserId")] public int UserId { get; set; }

        public Item(string name, string description, float buyPrice,
            float startingPrice, DateTime dateAdded, DateTime startingBidDate, DateTime endBidDate)
        {
            Name = name;
            Description = description;
            BuyPrice = buyPrice;
            StartingPrice = startingPrice;
            DateAdded = dateAdded;
            StartingBidDate = startingBidDate;
            EndBidDate = endBidDate;
        }
    }
}
