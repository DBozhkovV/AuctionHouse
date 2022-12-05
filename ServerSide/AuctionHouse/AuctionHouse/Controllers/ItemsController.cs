using AuctionHouse.DTOs;
using AuctionHouse.Models;
using AuctionHouse.Services.ItemService;
using Microsoft.AspNetCore.Mvc;

namespace AuctionHouse.Controllers
{
    [Route("items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IitemRepository itemRepository;

        public ItemsController(IitemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        [HttpGet]
        public IActionResult GetItems()
        {
            try
            {
                IEnumerable<Item> items = itemRepository.GetItems();
                if (items.Count() == 0) 
                {
                    return BadRequest("There is no Items.");
                }
                return Ok(items);
            }
            catch (Exception exception) 
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetItem(Guid id)
        {
            try
            {
                Item item = itemRepository.GetItem(id);
                if (item is null)
                {
                    return BadRequest("There is no item with given id.");
                }
                return Ok(item);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        public IActionResult PostItem(ItemDTO itemDTO)
        {
            try
            {
                if (HttpContext.Session.GetString("userId") is null) 
                {
                    return BadRequest("Don't have exist session.");
                }
                Guid userId = Guid.Parse(HttpContext.Session.GetString("userId"));
                itemRepository.PostItem(itemDTO, userId);
            }
            catch (Exception exception) 
            {
                return BadRequest(exception.Message);
            }
            return Ok();
        }

        [HttpPut("{id}/bid")]
        public IActionResult Bid(Guid id, BidDTO bidDTO) // da pitam milenkata
        {
            
            // proverka za sessiqta
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult<Item> BuyNow(Guid id) // Da pitam dali da ima async
        {
            try
            {
                if (HttpContext.Session.GetString("userId") is null)
                {
                    return BadRequest("Don't have exist session.");
                }
                Guid userId = Guid.Parse(HttpContext.Session.GetString("userId"));
                User user = itemRepository.FindUserByGuid(userId);
                Item item = itemRepository.BuyNow(id, user);
                return Ok(item);
            }
            catch (Exception exception) 
            {
                return BadRequest(exception.Message);
            }
        }
    
    }
}
