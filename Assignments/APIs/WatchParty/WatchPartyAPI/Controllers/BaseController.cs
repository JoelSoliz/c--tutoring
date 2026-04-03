using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using WatchPartyAPI.Exceptions;

namespace WatchPartyAPI.Controllers
{
    public abstract class BaseController : Controller
    {
        protected Guid GetUserIdFromToken()
        {
            var sub = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            if (sub == null || !Guid.TryParse(sub, out var userId))
                throw new UnauthorizedException("Invalid or missing token claims");
            return userId;
        }
    }
}
