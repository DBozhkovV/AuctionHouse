using AuctionHouse.DAOs.UserDAO;
using AuctionHouse.DTOs;
using AuctionHouse.Models;
using AuctionHouse.Services.AzureStorageService;
using AuctionHouse.Services.EmailService;
using AuctionHouse.Services.UserService;
using FakeItEasy;

namespace Test
{
    public class UserServiceTest
    {
        private readonly IUserRepository userRepository;
        private readonly IAzureStorageService azureStorageRepository;
        private readonly IEmailService emailService;
        private readonly IUserService userService;

        public UserServiceTest() 
        {
            userRepository = A.Fake<IUserRepository>();
            azureStorageRepository = A.Fake<IAzureStorageService>();
            emailService = A.Fake<IEmailService>();
            userService = new UserService(userRepository, azureStorageRepository, emailService);
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
        public void DeleteTest()
        {
            User user = userService.GetUserByUsername("UsernameTest");
            userService.DeleteUser(user.Id);
            Assert.Throws<Exception>(() => userService.GetUserByUsername("UsernameTest"));
        }
    }
}