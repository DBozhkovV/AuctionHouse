using AuctionHouse.DAOs.UserDAO;
using AuctionHouse.Data;
using AuctionHouse.DTOs;
using AuctionHouse.Models;
using AuctionHouse.Services.AzureStorageService;
using AuctionHouse.Services.EmailService;
using AuctionHouse.Services.UserService;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;

namespace Test
{
    public class UserServiceTest
    {
        private readonly IUserRepository userRepository;
        private readonly IAzureStorageService azureStorageRepository;
        private readonly IEmailService emailService;
        private readonly IUserService userService;
        private readonly DbContextOptions<DataContext> _options;

        public UserServiceTest() 
        {
            userRepository = A.Fake<IUserRepository>();
            azureStorageRepository = A.Fake<IAzureStorageService>();
            emailService = A.Fake<IEmailService>();
            userService = new UserService(userRepository, azureStorageRepository, emailService);
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "AuctionHouse")
                .Options;
        }
        
        [Fact]
        public void RegisterTest()
        {
            RegisterDTO registerDTO = new RegisterDTO() 
            {
                FirstName = "FirstNameTest",
                LastName = "LastNameTest",
                Username = "UsernameTest",
                Email = "EmailTest",
                Password = "PasswordTest",
                ConfirmPassword = "PasswordTest",
                PhoneNumber = "PhoneNumberTest"
            };
            Task result = userService.RegisterAsync(registerDTO);
            Assert.Equal(Task.CompletedTask, result);
        }

        [Fact]
        public void GetUserByUsername_ReturnNull() 
        {
            User user = userRepository.GetUserByUsername("UsernameTest");
            Assert.Null(user.Username);
        }
    }
}