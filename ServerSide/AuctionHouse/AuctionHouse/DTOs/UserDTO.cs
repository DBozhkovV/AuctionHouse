﻿using System.ComponentModel.DataAnnotations;

namespace AuctionHouse.DTOs
{
    public class UserDTO
    {
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        [Required] public string Username { get; set; }
        [Required] public string Email { get; set; }
        [Required] public string Password { get; set; }
        [Required] public string PhoneNumber { get; set; }
        public bool IsAdmin { get; set; } = false;
    }
}