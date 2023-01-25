using Microsoft.AspNetCore.Authorization;

namespace AuctionHouse.Services.CustomAuthorization
{
    public class SessionBasedAuthorizationHandler : AuthorizationHandler<SessionBasedAuthorizationRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public SessionBasedAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SessionBasedAuthorizationRequirement requirement)
        {
            string role = _httpContextAccessor.HttpContext.Session.GetString("Role");
            if (role is null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            if (role == requirement.Role.ToString())
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            else
            {
                context.Fail();
                return Task.CompletedTask;
            }
        }
    }
}
