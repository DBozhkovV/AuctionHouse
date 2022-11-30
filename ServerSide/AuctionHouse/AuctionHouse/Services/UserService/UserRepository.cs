using AuctionHouse.Data;
using AuctionHouse.DTOs;
using AuctionHouse.Models;

namespace AuctionHouse.Services.UserService
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext dataContext;
        public UserRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public void DeleteUser(int id)
        {
            User deletedUser = dataContext.Users.Where(o => o.Id == id).Single();
            dataContext.Users.Remove(deletedUser);
            dataContext.SaveChanges();
        }

        public void Login(LoginDTO loginDTO)
        {
            if (loginDTO is null)
            {
                throw new ArgumentNullException(nameof(loginDTO));
            }

            foreach (User user in dataContext.Users)
            {
                if (user.Username == loginDTO.Username)
                {
                    if (user.Password == BCrypt.Net.BCrypt.HashPassword(loginDTO.Password))
                    {
                        return; // Pravim sesiq
                    }
                }
            }
            throw new Exception("Invalid user.");
        }

        public void Register(UserDTO userDTO)
        {
            if (userDTO is null)
            {
                throw new ArgumentNullException(nameof(userDTO));
            }

            User user = new User()
            {
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Username = userDTO.Username,
                Email = userDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password),
                PhoneNumber = userDTO.PhoneNumber
            };
            dataContext.Users.Add(user);
            dataContext.SaveChanges();
        }
    }
}
