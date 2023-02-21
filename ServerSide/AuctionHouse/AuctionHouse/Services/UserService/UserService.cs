using AuctionHouse.DAOs.UserDAO;
using AuctionHouse.DTOs;
using AuctionHouse.Models;
using AuctionHouse.Services.AzureStorageService;
using System.Net;
using System.Net.Mail;

namespace AuctionHouse.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IAzureStorageService azureStorageRepository;

        public UserService(IUserRepository userRepository, IAzureStorageService azureStorageRepository)
        {
            this.userRepository = userRepository;
            this.azureStorageRepository = azureStorageRepository;
        }

        public void Register(RegisterDTO registerDTO)
        {
            if (registerDTO is null)
            {
                throw new ArgumentNullException(nameof(registerDTO));
            }

            if (!registerDTO.Password.Equals(registerDTO.ConfirmPassword)) 
            {
                throw new Exception("Invalid confirmed password");
            }

            if (userRepository.GetUserByUsername(registerDTO.Username) != null)
            {
                if (userRepository.GetUserByUsername(registerDTO.Username).Username == registerDTO.Username)
                {
                    throw new Exception("Username is already used.");
                }
            }
            
            if (userRepository.GetUserByEmail(registerDTO.Email) != null) 
            {
                if (userRepository.GetUserByEmail(registerDTO.Email).Email == registerDTO.Email)
                {
                    throw new Exception("This email is already used.");
                }
            }

            User newUser = new User()
            {
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                Username = registerDTO.Username,
                Email = registerDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password),
                PhoneNumber = registerDTO.PhoneNumber
            };
            userRepository.InsertUser(newUser);
        }

        public Guid Login(LoginDTO loginDTO)
        {
            if (loginDTO is null)
            {
                throw new ArgumentNullException(nameof(loginDTO));
            }

            User user = userRepository.GetUserByUsername(loginDTO.Username);
            if (user != null)
            {
                if (user.Username == loginDTO.Username)
                {
                    if (user.IsVerified == false)
                    {
                        throw new Exception("User is not verified.");
                    }
                    bool isValid = BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password);
                    if (isValid)
                    {
                        return user.Id;
                    }
                }
            }
            throw new Exception("Wrong username or password.");
        }

        public UserDTO Profile(Guid userId) 
        {
            User user = userRepository.GetUserById(userId);
            IEnumerable<Item> items = userRepository.GetItemsByUserId(userId);
            IEnumerable<Order> orders = userRepository.GetOrdersByUserId(userId);

            List<OrderDTO> orderDTOs = new List<OrderDTO>();
            foreach (Order order in orders)
            {
                OrderDTO orderDTO = new OrderDTO()
                {
                    Id = order.Id,
                    DateOrdered = order.DateOrdered,
                    IsOrderActive = order.IsOrderActive,
                    IsOrderCompleted = order.IsOrderCompleted,
                    ItemResponse = azureStorageRepository.ReturnItemResponse(userRepository.GetItemById(order.ItemId)),
                };
                orderDTOs.Add(orderDTO);
            }
            
            List<ItemResponse> itemDTOs = new List<ItemResponse>();
            foreach (Item item in items) 
            {
                ItemResponse itemResponse = azureStorageRepository.ReturnItemResponse(item);
                itemDTOs.Add(itemResponse);
            }

            UserDTO userDTO = new UserDTO()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Balance = user.Balance,
                Items = itemDTOs,
                Orders = orderDTOs
            };
            return userDTO;
        }

        public float GetBalanceByUserId(Guid userId)
        {
            return userRepository.GetBalanceByUserId(userId);
        }

        public void VerifyAccount(Guid token)
        {
            userRepository.VerifyUser(token);
        }

        public void ForgotPassword(string email) 
        {
            userRepository.ForgotPassword(email);
        }

        public async Task SendEmail(string toEmail)
        {
            /*
            using var smtpClient = new SmtpClient("smtp.sendgrid.net", 25)
            {
                Credentials = new NetworkCredential("ngdg", "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"),
                EnableSsl = false,
                UseDefaultCredentials = false
            };

            using var mailMessage = new MailMessage
            {
                From = new MailAddress("houseauction89@gmail.com", "Auction House"),
                Subject = "hi",
                Body = "<h1>Whatsup</h1>",
                IsBodyHtml = true
            };
            mailMessage.To.Add(new MailAddress(toEmail));

            await smtpClient.SendMailAsync(mailMessage);
            */
            var smtpServer = "smtp.sendgrid.net";
            var smtpPort = 25;
            var smtpUsername = "ngdg";
            var smtpPassword = "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";

            var client = new SmtpClient(smtpServer, smtpPort);
            client.UseDefaultCredentials = false;

            var from = new MailAddress("houseauction89@gmail.com", "AuctionHouse");
            var to = new MailAddress("danipaynera00@gmail.com", "Dakata RM");
            var subject = "Email Subject";
            var body = "Email Content";

            var message = new MailMessage(from, to);
            message.Subject = subject;
            message.Body = body;

            client.SendAsync(message, new NetworkCredential(smtpUsername, smtpPassword));
        }


        public void ResetPassword(ResetPasswordDTO resetPasswordDTO) 
        {
            User user = userRepository.GetUserByPassowordResetToken(resetPasswordDTO.Token);
            if (user.PasswordResetTokenExpires < DateTime.UtcNow)
            {
                throw new Exception("Password reset token has expired.");
            }

            userRepository.ResetPassword(resetPasswordDTO);
        }

        public void DeleteUser(Guid userId)
        {
            userRepository.DeleteUserById(userId);
        }
        
        public bool IsRoled(Guid userId, Role role) 
        {
            return userRepository.IsRoled(userId, role);
        }
        
    }
}
