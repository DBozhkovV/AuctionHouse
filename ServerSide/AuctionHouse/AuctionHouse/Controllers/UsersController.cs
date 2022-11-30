using AuctionHouse.Models;
using Microsoft.AspNetCore.Mvc;
using AuctionHouse.Data;

namespace AuctionHouse.Controllers
{
    [Route("users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext dataContext;

        public UsersController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpDelete]
        public IActionResult DeleteUser(int id)
        {
            User deletedUser = dataContext.Users.Where(o => o.Id == id).Single();
            dataContext.Users.Remove(deletedUser);
            dataContext.SaveChanges();
            return Ok();
        }

    }
}
