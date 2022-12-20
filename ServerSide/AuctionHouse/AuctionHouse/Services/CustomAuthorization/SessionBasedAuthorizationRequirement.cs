using AuctionHouse.Models;
using Microsoft.AspNetCore.Authorization;

namespace AuctionHouse.Services.CustomAuthorization
{
    public class SessionBasedAuthorizationRequirement : IAuthorizationRequirement
    {
        public Role Role { get; set; }

        public SessionBasedAuthorizationRequirement(Role role)
        {
            Role = role;
        }
    }
}
