using AuctionHouse.DTOs;
using AuctionHouse.Models;

namespace AuctionHouse.DAOs.UserDAO
{
    public interface IUserRepository
    {
        void insertUser(User user);

        User getUserByUsername(string username);

        User getUserByEmail(string email);

        User getUserById(Guid id);

        void verifyUser(Guid token);

        void forgotPassword(string email);

        User getUserByPassowordResetToken(Guid passwordResetToken);

        void resetPassword(ResetPasswordDTO resetPasswordDTO);

        void deleteUserById(Guid id);

        bool IsRoled(Guid userId, Role role);
    }
}
