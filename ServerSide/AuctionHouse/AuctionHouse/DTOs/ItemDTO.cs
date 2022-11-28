using System.ComponentModel.DataAnnotations;

namespace AuctionHouse.DTOs
{
    public class ItemDTO
    {
        [Required] public string Name { get; set; } = null!;
        [Required] public string Description { get; set; } = null!;
        [Required] public float BuyPrice { get; set; }
        [Required] public float StartingPrice { get; set; }
        [Required] public DateTime DateAdded { get; set; }
        [Required] public DateTime StartingBidDate { get; set; }
        [Required] public DateTime EndBidDate { get; set; }
        [Required] public int UserId { get; set; }
    }
}
