using AuctionHouse.DTOs;

namespace AuctionHouse.Services.UserService
{
    public interface IUserRepository
    {
        void Register(RegisterDTO registerDTO);
        Guid? Login(LoginDTO loginDTO);
        void DeleteUser(Guid userId);
    }
}
