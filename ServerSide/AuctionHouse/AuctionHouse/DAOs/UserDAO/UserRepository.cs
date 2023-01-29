using AuctionHouse.Data;
using AuctionHouse.DTOs;
using AuctionHouse.Models;

namespace AuctionHouse.DAOs.UserDAO
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;

        public UserRepository(DataContext _dataContext)
        {
            this._dataContext = _dataContext;
        }

        public void insertUser(User user)
        {
            _dataContext.Users.Add(user);
            _dataContext.SaveChanges();
        }

        public User getUserByUsername(string username)
        {
            return _dataContext.Users
                .Where(user => user.Username.Equals(username))
                .SingleOrDefault();
        }

        public User getUserByEmail(string email) 
        {
            return _dataContext.Users
                .Where(user => user.Email.Equals(email))
                .SingleOrDefault();
        }


        public User getUserById(Guid id)
        {
            return _dataContext.Users
                .Where(user => user.Id.Equals(id))
                .SingleOrDefault();
        }

        public void verifyUser(Guid token)
        {
            User user = _dataContext.Users
                .Where(user => user.VerificationToken.Equals(token))
                .SingleOrDefault();
            user.IsVerified = true;
            user.VerifiedAt = DateTime.UtcNow;
            _dataContext.SaveChanges();
        }

        public void forgotPassword(string email)
        {
            User user = _dataContext.Users
                .Where(user => user.Email.Equals(email))
                .SingleOrDefault();
            user.PasswordResetToken = Guid.NewGuid();
            user.PasswordResetTokenExpires = DateTime.UtcNow.AddDays(1);
            _dataContext.SaveChanges();
        }

        public User getUserByPassowordResetToken(Guid passwordResetToken)
        {
            return _dataContext.Users
                .Where(user => user.PasswordResetToken.Equals(passwordResetToken))
                .SingleOrDefault();
        }

        public void resetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            User user = getUserByPassowordResetToken(resetPasswordDTO.Token);
            user.Password = BCrypt.Net.BCrypt.HashPassword(resetPasswordDTO.Password);
            user.PasswordResetToken = null;
            user.PasswordResetTokenExpires = null;
            _dataContext.SaveChanges();
        }

        public void deleteUserById(Guid id)
        {
            User user = getUserById(id);
            _dataContext.Users.Remove(user);
            _dataContext.SaveChanges();
        }

        public bool IsRoled(Guid userId, Role role) 
        {
            User user = getUserById(userId);
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
