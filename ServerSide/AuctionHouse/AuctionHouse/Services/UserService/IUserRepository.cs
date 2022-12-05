using AuctionHouse.DTOs;

namespace AuctionHouse.Services.UserService
{
    public interface IUserRepository
    {
        void Register(RegisterDTO userDTO);
        Guid? Login(LoginDTO loginDTO);
        void DeleteUser(int id);
    }
}
