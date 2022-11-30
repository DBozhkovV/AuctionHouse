using AuctionHouse.Data;
using AuctionHouse.DTOs;
using AuctionHouse.Models;
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
                Bid = itemDTO.StartingPrice,
                UserId = itemDTO.UserId
            };
            dataContext.Items.Add(item);
            dataContext.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IActionResult Bid(BidDTO bidDTO) // da pitam milenkata
        {
            if (bidDTO is null)
            {
                throw new ArgumentNullException(nameof(bidDTO));
            }

            User BidUser = new User(); // da pitam nachalnika
            foreach (User user in dataContext.Users)
            {
                if (user.Id == bidDTO.BidUser)
                {
                    BidUser = user;
                }
            }

            Item BidItem = new Item(); // da pitam nachalnika
            foreach (Item item in dataContext.Items)
            {
                if (item.Id == bidDTO.ItemId)
                {
                    BidItem = item;
                }
            }

            if (BidUser.Money >= BidItem.Bid)
            {
                BidUser.Money -= BidItem.Bid;
                BidItem.Bid = bidDTO.Bid;
                dataContext.SaveChanges();
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult BuyNow(int id)
        {
            foreach (Item item in dataContext.Items)
            {
                if (item.Id == id)
                {
                    item.IsAvailable = false;
                    item.BoughtFor = item.BuyPrice;
                    item.EndBidDate = DateTime.Now;
                    dataContext.SaveChanges();
                    // parite na usera
                    return Ok();
                }
            }
            // Trqbva da se vzemem sesiqta na usera
            return BadRequest();
        }
    
    }
}
