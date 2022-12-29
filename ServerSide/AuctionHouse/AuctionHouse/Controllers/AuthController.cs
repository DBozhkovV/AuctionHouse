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

        [HttpPost]
        [Route("send-text-mail")]
        [AllowAnonymous]
        public async Task<IActionResult> SendPlainTextEmail(EmailDTO emailDTO)
        {
            // Replace YOUR_API_KEY with your actual SendGrid API key
            string apiKey = "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";
            var client = new SendGridClient(apiKey);

            // Generate a unique token for the user's email verification
            Guid guid = Guid.NewGuid();
            string emailVerificationToken = guid.ToString();

            // Store the email verification token and email address in a persistent store
            //StoreEmailVerificationToken(toEmail, emailVerificationToken);

            // Construct the email verification link
            string emailVerificationLink = $"https://example.com/verify-email?token={emailVerificationToken}";

            var message = new SendGridMessage();
            message.SetFrom("houseauction89@gmail.com", "Auction House");
            message.AddTo(emailDTO.Email, emailDTO.Name);
            message.SetSubject("Email Verification");
            message.AddContent(MimeType.Text, $"Click this link to verify your email address: {emailVerificationLink}");

            var response = await client.SendEmailAsync(message);

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                return Ok();
            }
            else
            {
                return BadRequest(response);
            }
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
            //HttpContext.Session.Remove("userId");
            //HttpContext.Session.Remove("Role");
            //HttpContext.Abort();
            return NoContent();
        }

        [HttpPut("verify/{token}")]
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

        [HttpPut("forgot-password")]
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

        [HttpPut("reset-password")]
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
