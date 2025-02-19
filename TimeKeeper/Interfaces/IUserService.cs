using System.Security.Claims;

namespace TimeKeeper.Interfaces

{
    public interface IUserService
    {
        public string GetUserId(ClaimsPrincipal user);

    }

}
