using AuctionHouse.Data;
using AuctionHouse.DTOs;
using AuctionHouse.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuctionHouse.Controllers
{
    [Route("items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly DataContext dataContext;

        public ItemsController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult GetItems() 
        {
            return Ok(dataContext.Items);
        }

        [HttpPost]
        public IActionResult PostItem(ItemDTO itemDTO) 
        {
            if (itemDTO is null)
            {
                throw new ArgumentNullException(nameof(itemDTO));
            }

            Item item = new Item()
            {
                Name = itemDTO.Name,
                Description = itemDTO.Description,
                BuyPrice = itemDTO.BuyPrice,
                StartingPrice = itemDTO.StartingPrice,
                DateAdded = itemDTO.DateAdded,
                StartingBidDate = itemDTO.StartingBidDate,
                EndBidDate = itemDTO.EndBidDate,
                UserId = itemDTO.UserId
            };
            dataContext.Items.Add(item);
            dataContext.SaveChanges();
            return Ok();
        }
    }
}
