using System.Security.Claims;
using TimeKeeper.Interfaces;

namespace TimeKeeper.Services
{
    public class UserService : IUserService
    {

        public string GetUserId(ClaimsPrincipal user)
        {

            //var user = _httpContextAccessor.HttpContext?.User;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                throw new Exception("User not authenticated");
            }

            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("User ID is missing");
            }

            return userId;
        }
    }
}
