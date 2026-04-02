using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace WatchTrackerAPI.Controllers
{
    public class BaseController : Controller
    {
        protected Guid? GetUserIdFromToken()
        {
            var sub = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            if (sub == null || !Guid.TryParse(sub, out var userId)) return null;
            return userId;
        }
    }
}
