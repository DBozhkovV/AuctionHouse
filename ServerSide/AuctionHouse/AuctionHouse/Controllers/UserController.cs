using AuctionHouse.DTOs;
using Microsoft.AspNetCore.Mvc;
using AuctionHouse.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using AuctionHouse.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AuctionHouse.Controllers
{
    [Route("")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        [Route("send-text-mail")]
        [AllowAnonymous]
        public async Task<IActionResult> SendPlainTextEmail(EmailDTO emailDTO)
        {
            // Name = AuctionHouse
            // Email = houseauction89@gmail.com
            try
            {
                await userService.SendEmail("danipaynera00@gmail.com");
            }
            catch (Exception exception)
            {
                return BadRequest(exception.ToString());
            }
            return Ok();
        }
        
        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register(RegisterDTO registerDTO)
        {
            try 
            {
                userService.Register(registerDTO);
            }
            catch (Exception exception) 
            {
                return BadRequest(exception.Message);
            }
            return Ok();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login(LoginDTO loginDTO) 
        {
            try
            {
                Guid? userId = userService.Login(loginDTO);
                if (userId is null)
                {
                    return BadRequest();
                }
                HttpContext.Session.SetString("userId", userId.ToString());
                if (userService.IsRoled(userId.Value, Role.User))
                {
                    HttpContext.Session.SetString("Role", "User");
                }
                else 
                {
                    HttpContext.Session.SetString("Role", "Admin");
                }
            }
            catch (Exception exception) 
            {
                return BadRequest(exception.Message);
            }
            return Ok();
        }

        [HttpPost("logout")]
        [AllowAnonymous]
        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("userId") is null)
            {
                return BadRequest("Don't have exist session.");
            }
            Response.Cookies.Delete("ASP");
            HttpContext.Session.Clear();
            return NoContent();
        }

        [HttpPut("verify/{token}")]
        [AllowAnonymous]
        public IActionResult VerifyAccount(Guid token)
        {
            try
            {
                userService.VerifyAccount(token);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
            return Ok();
        }

        [HttpPut("forgot-password")]
        [AllowAnonymous]
        public IActionResult ForgotPassword(string email) 
        {
            try
            {
                userService.ForgotPassword(email);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
            return Ok();
        }

        [HttpPut("reset-password")]
        [AllowAnonymous]
        public IActionResult ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            try
            {
                userService.ResetPassword(resetPasswordDTO);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
            return Ok();
        }

        [HttpGet("isUser")]
        [AllowAnonymous]
        public IActionResult IsUser()
        {
            if (HttpContext.Session.GetString("Role") is null)
            {
                return BadRequest("Don't have exist session.");
            }
            if (HttpContext.Session.GetString("Role") == "User")
            {
                return Ok(userService.GetBalanceByUserId(Guid.Parse(HttpContext.Session.GetString("userId"))));
            }
            return BadRequest("You are not admin.");
        }

        [HttpGet("isAdmin")]
        [AllowAnonymous]
        public IActionResult IsAdmin()
        {
            if (HttpContext.Session.GetString("Role") is null)
            {
                return BadRequest("Don't have exist session.");
            }
            if (HttpContext.Session.GetString("Role") == "Admin")
            {
                return Ok();
            }
            return BadRequest("You are not admin.");
        }

        [HttpGet("profile")]
        [Authorize(Policy = "User")]
        public IActionResult Profile() 
        {
            if (HttpContext.Session.GetString("userId") is null) 
            {
                return Unauthorized();
            }
            try
            {
                Guid userId = Guid.Parse(HttpContext.Session.GetString("userId"));
                UserDTO userDTO = userService.Profile(userId);
                return Ok(userDTO);
            } catch (Exception exception) 
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete]
        [Authorize(Policy = "User")]
        public IActionResult DeleteUser()
        {
            try 
            {
                if (HttpContext.Session.GetString("userId") is null)
                {
                    return BadRequest("Don't have exist session.");
                }
                Guid userId = Guid.Parse(HttpContext.Session.GetString("userId"));
                userService.DeleteUser(userId);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
            return Ok();
        }
    }
}
