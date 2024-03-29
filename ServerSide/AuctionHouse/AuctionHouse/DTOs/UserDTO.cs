﻿namespace AuctionHouse.DTOs
{
    public class UserDTO
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Username { get; set; }
        
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public float Balance { get; set; }

        public IEnumerable<ItemResponse>? Items { get; set; } = null;

        public IEnumerable<OrderDTO>? Orders { get; set; } = null;
    }
}
