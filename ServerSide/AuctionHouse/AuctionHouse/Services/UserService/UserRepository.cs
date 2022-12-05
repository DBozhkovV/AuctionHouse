using AuctionHouse.Data;
using AuctionHouse.DTOs;
using AuctionHouse.Models;
using Microsoft.AspNetCore.Http;

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
            User deletedUser = dataContext.Users.Where(o => o.Id.Equals(id)).Single();
            dataContext.Users.Remove(deletedUser);
            dataContext.SaveChanges();
        }

        public Guid? Login(LoginDTO loginDTO)
        {
            if (loginDTO is null)
            {
                throw new ArgumentNullException(nameof(loginDTO));
            }

            foreach (User user in dataContext.Users)
            {
                if (user.Username == loginDTO.Username)
                {
                    bool isValid = BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password);
                    if (isValid)
                    {
                        return user.Id;
                    }
                }
            }
            return null;
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

            foreach (User userI in dataContext.Users)
            {
                if (userI.Username == registerDTO.Username)
                {
                    throw new Exception("Username is already used.");
                }
            }

            User user = new User()
            {
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                Username = registerDTO.Username,
                Email = registerDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password),
                PhoneNumber = registerDTO.PhoneNumber
            };
            dataContext.Users.Add(user);
            dataContext.SaveChanges();
        }
    }
}
