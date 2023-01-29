using AuctionHouse.DAOs.UserDAO;
using AuctionHouse.DTOs;
using AuctionHouse.Models;

namespace AuctionHouse.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
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

            if (userRepository.getUserByUsername(registerDTO.Username) != null)
            {
                if (userRepository.getUserByUsername(registerDTO.Username).Username == registerDTO.Username)
                {
                    throw new Exception("Username is already used.");
                }
            }
            
            if (userRepository.getUserByEmail(registerDTO.Email) != null) 
            {
                if (userRepository.getUserByEmail(registerDTO.Email).Email == registerDTO.Email)
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
            userRepository.insertUser(newUser);
        }

        public Guid? Login(LoginDTO loginDTO)
        {
            if (loginDTO is null)
            {
                throw new ArgumentNullException(nameof(loginDTO));
            }

            User user = userRepository.getUserByUsername(loginDTO.Username);
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
            User user = userRepository.getUserById(userId);
            UserDTO userDTO = new UserDTO()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Balance = user.Balance
            };
            return userDTO;
        }
        
        public void VerifyAccount(Guid token)
        {
            userRepository.verifyUser(token);
        }

        public void ForgotPassword(string email) 
        {
            userRepository.forgotPassword(email);
        }

        public void ResetPassword(ResetPasswordDTO resetPasswordDTO) 
        {
            User user = userRepository.getUserByPassowordResetToken(resetPasswordDTO.Token);
            if (user.PasswordResetTokenExpires < DateTime.UtcNow)
            {
                throw new Exception("Password reset token has expired.");
            }

            userRepository.resetPassword(resetPasswordDTO);
        }

        public void DeleteUser(Guid userId)
        {
            userRepository.deleteUserById(userId);
        }
        
        public bool IsRoled(Guid userId, Role role) 
        {
            return userRepository.IsRoled(userId, role);
        }
        
    }
}
