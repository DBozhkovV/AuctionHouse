using AuctionHouse.DTOs;
using Microsoft.AspNetCore.Mvc;
using AuctionHouse.Services.UserService;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.AspNetCore.Authorization;
using AuctionHouse.Models;

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

        [HttpGet]
        [Route("send-text-mail")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> SendPlainTextEmail(string toEmail, string name)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("houseauction89@gmail.com", "AuctionHouse"),
                Subject = "Sending with Twilio SendGrid is Fun",
                PlainTextContent = "and easy to do anywhere, especially with C#"
            };
            msg.AddTo(new EmailAddress(toEmail, name));
            var response = await client.SendEmailAsync(msg);
            string message = response.IsSuccessStatusCode ? "Email Send Successfully" :
            "Email Sending Failed";
            return Ok(message);
        }

        
        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register(RegisterDTO registerDTO)
        {
            try 
            {
                userRepository.Register(registerDTO);
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
                Guid? userId = userRepository.Login(loginDTO);
                if (userId is null)
                {
                    return BadRequest();
                }
                HttpContext.Session.SetString("userId", userId.ToString());
                if (userRepository.IsRoled(userId.Value, Role.User))
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
        [Authorize(Policy = "User")]
        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("userId") is null)
            {
                return BadRequest("Don't have exist session.");
            }
            HttpContext.Session.Clear(); // da proverq kak bachka
            HttpContext.Session.Remove("userId");
            HttpContext.Session.Remove("Role");
            return Ok();
        }

        [HttpPost("verify/{token}")]
        [AllowAnonymous]
        public IActionResult VerifyAccount(Guid token)
        {
            try
            {
                userRepository.VerifyAccount(token);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
            return Ok();
        }

        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public IActionResult ForgotPassword(string email) 
        {
            try
            {
                userRepository.ForgotPassword(email);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
            return Ok();
        }

        [HttpPost("reset-password")]
        [AllowAnonymous]
        public IActionResult ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            try
            {
                userRepository.ResetPassword(resetPasswordDTO);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
            return Ok();
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
                userRepository.DeleteUser(userId);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
            return Ok();
        }
    }
}
