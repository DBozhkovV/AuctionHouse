using AuctionHouse.DTOs;

namespace AuctionHouse.Services.UserService
{
    public interface IUserRepository
    {
        void Register(UserDTO userDTO);
        void Login(LoginDTO loginDTO);
        void DeleteUser(int id);
    }
}
