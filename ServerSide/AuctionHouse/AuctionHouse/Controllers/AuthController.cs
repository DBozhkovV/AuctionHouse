using AuctionHouse.DTOs;
using Microsoft.AspNetCore.Mvc;
using AuctionHouse.Services.UserService;

namespace AuctionHouse.Controllers
{
    [Route("")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserRepository userRepository;

        public AuthController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDTO userDTO)
        {
            try 
            {
                userRepository.Register(userDTO);
            }
            catch (Exception exception) 
            {
                return BadRequest(exception.Message);
            }
            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO loginDTO) 
        {
            try
            {
                Guid? userId = userRepository.Login(loginDTO);
                if (userId is null)
                {
                    return BadRequest();
                }
                //String stringToSave = JsonSerializer.Serialize(userDTO); // Transform object to String
                HttpContext.Session.SetString("userId", userId.ToString());
            }
            catch (Exception exception) 
            {
                return BadRequest(exception.Message);
            }
            return Ok();
        }
    }
}
