﻿using AuctionHouse.DTOs;
using AuctionHouse.Models;
using Microsoft.AspNetCore.Mvc;
using AuctionHouse.Services.UserService;
using AuctionHouse.Data;

namespace AuctionHouse.Controllers
{
    [Route("")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext dataContext;

        public AuthController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpPost("register")]
        public IActionResult Register(UserDTO userDTO)
        {
            if (userDTO is null)
            {
                throw new ArgumentNullException(nameof(userDTO));
            }

            User user = new User()
            {
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Username = userDTO.Username,
                Email = userDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password),
                PhoneNumber = userDTO.PhoneNumber
            };
            dataContext.Users.Add(user);
            dataContext.SaveChanges();
            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO loginDTO) 
        {
            if (loginDTO is null)
            {
                throw new ArgumentNullException(nameof(loginDTO));
            }

            foreach (User user in dataContext.Users)
            {
                if (user.Username == loginDTO.Username)
                {
                    var isValid = BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password);
                    if (isValid)
                    {
                        return Ok(); // Pravim sesiq
                    }
                }
            }
            return BadRequest();
        }

    }
}
