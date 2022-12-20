using AuctionHouse.DTOs;
using AuctionHouse.Models;
using AuctionHouse.Services.ItemService;
using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        public IActionResult GetAvailableItems()
        {
            try
            {
                IEnumerable<Item> items = itemRepository.GetAvailableItems();
                if (items.Count() == 0) 
                {
                    return BadRequest("There is no available items.");
                }
                return Ok(items);
            }
            catch (Exception exception) 
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("not-accepted")]
        [Authorize(Policy = "Admin")]
        public IActionResult GetNotAcceptedItems()
        {
            try
            {
                IEnumerable<Item> items = itemRepository.GetNotAcceptedItems();
                if (items.Count() == 0)
                {
                    return BadRequest("There is no not-accepted items.");
                }
                return Ok(items);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("search")]
        [AllowAnonymous]
        public IActionResult SearchItems(string search)
        {
            try
            {
                IEnumerable<Item> items = itemRepository.SearchItems(search);
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
        [AllowAnonymous]
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
        [Authorize(Policy = "User")]
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
        [Authorize(Policy = "User")]
        public IActionResult Bid(Guid id, [FromBody] float Bid) // da pitam milenkata
        {
            
            // proverka za sessiqta
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<Item>> BuyNowAsync(Guid id) // Da dobavq await nqkude tuk
        {
            try
            {
                if (HttpContext.Session.GetString("userId") is null)
                {
                    return BadRequest("Don't have exist session.");
                }
                Guid userId = Guid.Parse(HttpContext.Session.GetString("userId"));
                User user = itemRepository.FindUserByGuid(userId);
                Item item = await itemRepository.BuyNowAsync(id, user);
                return Ok(item);
            }
            catch (Exception exception) 
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
