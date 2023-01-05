using AuctionHouse.DTOs;
using AuctionHouse.Models;

namespace AuctionHouse.Services.UserService
{
    public interface IUserRepository
    {
        void Register(RegisterDTO registerDTO);
        Guid? Login(LoginDTO loginDTO);
        UserDTO Profile(Guid userId);
        void VerifyAccount(Guid token);
        void ForgotPassword(string email);
        void ResetPassword(ResetPasswordDTO resetPasswordDTO);
        void DeleteUser(Guid userId);
        bool IsRoled(Guid userId, Role role);
    }
}
