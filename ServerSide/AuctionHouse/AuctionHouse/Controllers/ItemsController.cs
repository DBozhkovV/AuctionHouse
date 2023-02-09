using AuctionHouse.DTOs;
using AuctionHouse.Models;
using AuctionHouse.Services.AzureStorageService;
using AuctionHouse.Services.ItemService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuctionHouse.Controllers
{
    [Route("items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService itemService;

        public ItemsController(IItemService itemService)
        {
            this.itemService = itemService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAvailableItems()
        {
            try
            {
                IEnumerable<Task<ItemResponse>> items = itemService.GetAvailableItems();
                if (!items.Any()) 
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
                IEnumerable<Task<ItemResponse>> items = itemService.GetNotAcceptedItems();
                if (!items.Any())
                {
                    return BadRequest("There is no notaccpeted items.");
                }
                return Ok(items);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("not-accepted/{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetNotAcceptedItem(Guid id)
        {
            try
            {
                ItemResponse itemResponse = await itemService.GetNotAcceptedItem(id);
                return Ok(itemResponse);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut("accept/{id}")]
        [Authorize(Policy = "Admin")]
        public IActionResult AcceptItem(Guid id) 
        {
            try
            {
                itemService.AcceptItem(id);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut("reject/{id}")]
        [Authorize(Policy = "Admin")]
        public IActionResult RejectItem(Guid id)
        {
            try
            {
                itemService.RejectItem(id);
                return Ok();
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
                IEnumerable<Task<ItemResponse>> items = itemService.SearchItems(search);
                if (!items.Any())
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
        [HttpGet("category")]
        [AllowAnonymous]
        public IActionResult GetItemsByCategory(Category category)
        {
            try
            {
                IEnumerable<Task<ItemResponse>> items = itemService.GetItemsByCategory(category);
                if (!items.Any())
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
        public async Task<IActionResult> GetItem(Guid id)
        {
            try
            {
                ItemResponse itemResponse = await itemService.GetItem(id);
                if (itemResponse is null)
                {
                    return BadRequest("There is no item with given id.");
                }
                return Ok(itemResponse);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet]
        [Route("newest")]
        [AllowAnonymous]
        public IActionResult GetNewestItems()
        {
            try
            {
                IEnumerable<Task<ItemResponse>> items = itemService.GetFiveNewestItems();
                if (!items.Any())
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

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 5_000_000)] // Set request limit of 5MB
        [Authorize(Policy = "User")]
        public IActionResult PostItem([FromForm] ItemDTO itemDTO)
        {
            try
            {
                if (HttpContext.Session.GetString("userId") is null) 
                {
                    return BadRequest("Don't have exist session.");
                }
                Guid userId = Guid.Parse(HttpContext.Session.GetString("userId"));
                itemService.PostItem(itemDTO, userId);
            }
            catch (Exception exception) 
            {
                return BadRequest(exception.Message);
            }
            return Ok();
        }

        [HttpPut("bid")]
        [Authorize(Policy = "User")]
        public IActionResult Bid(BidDTO bidDTO)
        {

            try 
            {
                if (HttpContext.Session.GetString("userId") is null)
                {
                    return BadRequest("Don't have exist session.");
                }
                Guid userId = Guid.Parse(HttpContext.Session.GetString("userId"));
                itemService.Bid(bidDTO.ItemId, userId, bidDTO.Money);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
            return Ok();
        }

        [HttpPut("buy/{id}")]
        [Authorize(Policy = "User")]
        public ActionResult<Item> BuyNow(Guid id)
        {
            try
            {
                if (HttpContext.Session.GetString("userId") is null)
                {
                    return BadRequest("Don't have exist session.");
                }
                Guid userId = Guid.Parse(HttpContext.Session.GetString("userId"));
                User user = itemService.FindUserByGuid(userId);
                Item item = itemService.BuyNow(id, user);
                return Ok();
            }
            catch (Exception exception) 
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
