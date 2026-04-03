using WatchPartyAPI.Models;

namespace WatchPartyAPI.Interfaces.Services
{
    public interface IJwtTokenService
    {
        public string GenerateToken(User user);
    }
}
