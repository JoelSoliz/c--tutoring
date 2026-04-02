using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchTrackerAPI.DTOs.Parameters;
using WatchTrackerAPI.Interfaces.Services;

namespace WatchTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/users/stats")]
    [Authorize(Roles = "User")]
    public class UserStatsController : BaseController
    {
        private readonly IUserStatsService _userStatsService;

        public UserStatsController(IUserStatsService userStatsService)
        {
            _userStatsService = userStatsService;
        }

        [HttpGet("top-anime")]
        public async Task<IActionResult> GetTopCompletedAnimes([FromQuery] TopRankingQueryParams animeParams)
        {
            var userId = GetUserIdFromToken();
            if (userId == null) return Unauthorized();
            var topAnime = await _userStatsService.GetTopAnimes(userId.Value, animeParams);
            return Ok(topAnime);
        }

        [HttpGet("monthly-top")]
        public async Task<IActionResult> GetMonthlyRanking([FromQuery] TopRankingQueryParams rankingParams)
        {
            var userId = GetUserIdFromToken();
            if (userId == null) return Unauthorized();
            var ranking = await _userStatsService.GetMonthlyRanking(userId.Value, rankingParams);
            return Ok(ranking);
        }
    }

}
