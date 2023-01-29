using AuctionHouse.DTOs;
using AuctionHouse.Models;
using AuctionHouse.Services.AzureStorageService;
using AuctionHouse.Services.ItemService;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;

namespace AuctionHouse.Controllers
{
    [Route("items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService _itemService)
        {
            this._itemService = _itemService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAvailableItems()
        {
            try
            {
                IEnumerable<Task<ItemResponse>> items = _itemService.GetAvailableItems();
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
                IEnumerable<Task<ItemResponse>> items = _itemService.GetNotAcceptedItems();
                if (items.Count() == 0)
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
                ItemResponse itemResponse = await _itemService.GetNotAcceptedItem(id);
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
                _itemService.AcceptItem(id);
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
                _itemService.RejectItem(id);
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
                IEnumerable<Task<ItemResponse>> items = _itemService.SearchItems(search);
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
        [HttpGet("category")]
        [AllowAnonymous]
        public IActionResult GetItemsByCategory(Category category)
        {
            try
            {
                IEnumerable<Task<ItemResponse>> items = _itemService.GetItemsByCategory(category);
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
        public async Task<IActionResult> GetItem(Guid id)
        {
            try
            {
                ItemResponse itemResponse = await _itemService.GetItem(id);
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

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 1_000_000)] // Set request limit of 1MB
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
                _itemService.PostItem(itemDTO, userId);
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
        public ActionResult<Item> BuyNowAsync(Guid id) // Da dobavq await nqkude tuk
        {
            try
            {
                if (HttpContext.Session.GetString("userId") is null)
                {
                    return BadRequest("Don't have exist session.");
                }
                Guid userId = Guid.Parse(HttpContext.Session.GetString("userId"));
                User user = _itemService.FindUserByGuid(userId);
                Item item = _itemService.BuyNow(id, user);
                return Ok(item);
            }
            catch (Exception exception) 
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
