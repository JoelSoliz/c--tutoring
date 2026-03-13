using Microsoft.AspNetCore.Mvc;
using WatchTrackerAPI.DTOs;
using WatchTrackerAPI.Interfaces;

namespace WatchTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/users/{userId}")]
    public class UserStatsController : Controller
    {
        private readonly IUserStatsService _userStatsService;

        public UserStatsController(IUserStatsService userStatsService)
        {
            _userStatsService = userStatsService;
        }

        [HttpGet("stats/top-anime")]
        public async Task<IActionResult> GetTopCompletedAnimes([FromRoute] Guid userId, [FromQuery] TopAnimeQueryParams animeParams)
        {
            var topAnime = await _userStatsService.GetTopAnimes(userId, animeParams);
            return Ok(topAnime);
        }
    }
}
