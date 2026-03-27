using WatchTrackerAPI.Models.Entities;

namespace WatchTrackerAPI.Interfaces.Services
{
    public interface IJwtTokenService
    {
        public string GenerateToken(User user);
    }
}
