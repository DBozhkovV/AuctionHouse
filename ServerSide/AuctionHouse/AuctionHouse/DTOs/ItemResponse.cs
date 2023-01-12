using Microsoft.AspNetCore.Mvc;

namespace AuctionHouse.DTOs
{
    public class ItemResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public float BuyPrice { get; set; }
        
        public float StartingPrice { get; set; }
        
        public float Bid { get; set; } = 0;
        
        public DateTime DateAdded { get; set; }
        
        public DateTime StartingBidDate { get; set; }
        
        public DateTime EndBidDate { get; set; }
        
        public byte[] MainImage { get; set; }
        
        public List<byte[]> Images { get; set; }

        public ItemResponse() 
        {
        }
        
    }
}
