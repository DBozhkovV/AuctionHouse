using AuctionHouse.DTOs;
using AuctionHouse.Models;

namespace AuctionHouse.DAOs.UserDAO
{
    public interface IUserRepository
    {
        void InsertUser(User user);

        User GetUserByUsername(string username);

        User GetUserByUsernameForRegister(string username);

        User GetUserByEmail(string email);

        User GetUserById(Guid id);

        IEnumerable<Order> GetOrdersByUserId(Guid userId);

        IEnumerable<Item> GetItemsByUserId(Guid userId);

        Item GetItemById(Guid id);

        float GetBalanceByUserId(Guid userId);

        void VerifyUser(Guid token);

        void ForgotPassword(string email);

        User GetUserByPassowordResetToken(Guid passwordResetToken);

        void ResetPassword(ResetPasswordDTO resetPasswordDTO);

        void DeleteUserById(Guid id);

        bool IsRoled(Guid userId, Role role);
    }
}
