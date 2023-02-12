using AuctionHouse.DAOs.UserDAO;
using AuctionHouse.DTOs;
using AuctionHouse.Models;
using AuctionHouse.Services.AzureStorageService;
using Hangfire;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AuctionHouse.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IAzureStorageRepository azureStorageRepository;

        public UserService(IUserRepository userRepository, IAzureStorageRepository azureStorageRepository)
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

        public Guid? Login(LoginDTO loginDTO)
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
            
            List<Task<ItemResponse>> itemDTOs = new List<Task<ItemResponse>>();
            foreach (Item item in items) 
            {
                Task<ItemResponse> itemResponse = azureStorageRepository.ReturnItemResponse(item);
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
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("houseauction89@gmail.com", "AuctionHouse");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress(toEmail);
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            await client.SendEmailAsync(msg);
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
