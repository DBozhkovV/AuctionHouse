using System.ComponentModel.DataAnnotations;

namespace AuctionHouse.DTOs
{
    public class BidDTO
    {
        [Required] public int ItemId { get; set; }
        [Required] public float Bid { get; set; }
        [Required] public int BidUser { get; set; }

    }
}
