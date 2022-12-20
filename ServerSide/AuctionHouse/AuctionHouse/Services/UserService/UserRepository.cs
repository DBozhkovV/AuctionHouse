using AuctionHouse.Data;
using AuctionHouse.DTOs;
using AuctionHouse.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AuctionHouse.Services.UserService
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext dataContext;
        public UserRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
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

                if (userI.Email == registerDTO.Email) 
                {
                    throw new Exception("This email is already used.");
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
            return null;
        }

        public void VerifyAccount(Guid token)
        {
            User user = dataContext.Users.Where(u => u.VerificationToken == token).Single();
            user.IsVerified = true;
            user.VerifiedAt = DateTime.UtcNow;
            dataContext.SaveChanges();
        }

        public void ForgotPassword(string email) 
        {
            User user = dataContext.Users.Where(u => u.Email == email).Single();
            user.PasswordResetToken = Guid.NewGuid();
            user.PasswordResetTokenExpires = DateTime.UtcNow.AddDays(1);
            dataContext.SaveChanges();
        }

        public void ResetPassword(ResetPasswordDTO resetPasswordDTO) 
        {
            User user = dataContext.Users.Where(u => u.PasswordResetToken == resetPasswordDTO.Token).Single();
            if (user.PasswordResetTokenExpires < DateTime.UtcNow)
            {
                throw new Exception("Password reset token has expired.");
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(resetPasswordDTO.Password);
            user.PasswordResetToken = null;
            user.PasswordResetTokenExpires = null;
            dataContext.SaveChanges();
        }

        public void DeleteUser(Guid userId)
        {
            User deletedUser = dataContext.Users.Where(o => o.Id.Equals(userId)).Single();
            dataContext.Users.Remove(deletedUser);
            dataContext.SaveChanges();
        }

        public bool IsRoled(Guid userId, Role role) 
        {
            User user = dataContext.Users.Where(o => o.Id == userId).Single();
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
