using AuctionHouse.DTOs;
using AuctionHouse.Models;

namespace AuctionHouse.Services.UserService
{
    public interface IUserService
    {
        void Register(RegisterDTO registerDTO);
        
        Guid Login(LoginDTO loginDTO);
        
        UserDTO Profile(Guid userId);

        float GetBalanceByUserId(Guid userId);

        void VerifyAccount(Guid token);
        
        void ForgotPassword(string email);

        Task SendEmail(string toEmail);

        void ResetPassword(ResetPasswordDTO resetPasswordDTO);
        
        void DeleteUser(Guid userId);
       
        bool IsRoled(Guid userId, Role role);
    }
}
