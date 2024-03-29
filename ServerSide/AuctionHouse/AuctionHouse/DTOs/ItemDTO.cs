﻿using AuctionHouse.Models;

namespace AuctionHouse.DTOs
{
    public class ItemDTO
    {
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public float BuyPrice { get; set; }
        
        public float StartingPrice { get; set; }
        
        public DateTime DateAdded { get; set; }
        
        public DateTime StartingBidDate { get; set; }
        
        public DateTime EndBidDate { get; set; }
        
        public Category Category { get; set; }
        
        public IFormFile MainImage { get; set; }
        
        public IFormFile[] Images { get; set; }
    }
}
