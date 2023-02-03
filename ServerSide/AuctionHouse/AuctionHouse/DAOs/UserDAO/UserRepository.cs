using AuctionHouse.Data;
using AuctionHouse.DTOs;
using AuctionHouse.Models;

namespace AuctionHouse.DAOs.UserDAO
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext dataContext;

        public UserRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public void InsertUser(User user)
        {
            dataContext.Users.Add(user);
            dataContext.SaveChanges();
        }

        public User GetUserByUsername(string username)
        {
            return dataContext.Users
                .Where(user => user.Username.Equals(username))
                .SingleOrDefault();
        }

        public User GetUserByEmail(string email) 
        {
            return dataContext.Users
                .Where(user => user.Email.Equals(email))
                .SingleOrDefault();
        }


        public User GetUserById(Guid id)
        {
            return dataContext.Users
                .Where(user => user.Id.Equals(id))
                .SingleOrDefault();
        }

        public IEnumerable<Order> GetOrdersByUserId(Guid userId) 
        {
            return dataContext.Orders
                .Where(order => order.UserId.Equals(userId))
                .ToList();
        }

        public Item GetItemById(Guid id)
        {
            Item item = dataContext.Items
                .Where(item => item.Id.Equals(id))
                .SingleOrDefault();
            if (item == null)
            {
                throw new Exception("Item not found");
            }
            return item;
        }

        public float GetBalanceByUserId(Guid userId)
        {
            return dataContext.Users
                .Single(user => user.Id.Equals(userId))
                .Balance;
        }

        public IEnumerable<Item> GetItemsByUserId(Guid userId) 
        {
            return dataContext.Items
                .Where(item => item.AuthorUserId.Equals(userId) && item.IsAvailable == true)
                .ToList();
        }

        public void VerifyUser(Guid token)
        {
            User user = dataContext.Users
                .Where(user => user.VerificationToken.Equals(token))
                .SingleOrDefault();
            user.IsVerified = true;
            user.VerifiedAt = DateTime.UtcNow;
            dataContext.SaveChanges();
        }

        public void ForgotPassword(string email)
        {
            User user = dataContext.Users
                .Where(user => user.Email.Equals(email))
                .SingleOrDefault();
            user.PasswordResetToken = Guid.NewGuid();
            user.PasswordResetTokenExpires = DateTime.UtcNow.AddDays(1);
            dataContext.SaveChanges();
        }

        public User GetUserByPassowordResetToken(Guid passwordResetToken)
        {
            return dataContext.Users
                .Where(user => user.PasswordResetToken.Equals(passwordResetToken))
                .SingleOrDefault();
        }

        public void ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            User user = GetUserByPassowordResetToken(resetPasswordDTO.Token);
            user.Password = BCrypt.Net.BCrypt.HashPassword(resetPasswordDTO.Password);
            user.PasswordResetToken = null;
            user.PasswordResetTokenExpires = null;
            dataContext.SaveChanges();
        }

        public void DeleteUserById(Guid id)
        {
            User user = GetUserById(id);
            dataContext.Users.Remove(user);
            dataContext.SaveChanges();
        }

        public bool IsRoled(Guid userId, Role role) 
        {
            User user = GetUserById(userId);
            if (user.Role == role)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
