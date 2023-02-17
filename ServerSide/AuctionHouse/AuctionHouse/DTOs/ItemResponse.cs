namespace AuctionHouse.DTOs
{
    public class ItemResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public float BuyPrice { get; set; }
        
        public float StartingPrice { get; set; }

        public float BoughtFor { get; set; }

        public bool IsAccepted { get; set; }

        public bool IsAvailable { get; set; }

        public float Bid { get; set; } = 0;
        
        public DateTime DateAdded { get; set; }
        
        public DateTime StartingBidDate { get; set; }
        
        public DateTime EndBidDate { get; set; }
        
        public string MainImage { get; set; }
        
        public List<string> Images { get; set; }

        public ItemResponse() 
        {
        }
        
    }
}
