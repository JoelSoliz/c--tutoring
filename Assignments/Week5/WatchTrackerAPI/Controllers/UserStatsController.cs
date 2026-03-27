using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using WatchTrackerAPI.DTOs.Parameters;
using WatchTrackerAPI.Interfaces.Services;

namespace WatchTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/users/{userId}/stats")]
    [Authorize(Roles = "User")]
    public class UserStatsController : Controller
    {
        private readonly IUserStatsService _userStatsService;

        public UserStatsController(IUserStatsService userStatsService)
        {
            _userStatsService = userStatsService;
        }

        [HttpGet("top-anime")]
        public async Task<IActionResult> GetTopCompletedAnimes([FromRoute] Guid userId, [FromQuery] TopRankingQueryParams animeParams)
        {
            if (!isOwner(userId)) return Forbid();
            var topAnime = await _userStatsService.GetTopAnimes(userId, animeParams);
            return Ok(topAnime);
        }

        [HttpGet("monthly-top")]
        public async Task<IActionResult> GetMonthlyRanking([FromRoute] Guid userId, [FromQuery] TopRankingQueryParams rankingParams)
        {
            if (!isOwner(userId)) return Forbid();
            var ranking = await _userStatsService.GetMonthlyRanking(userId, rankingParams);
            return Ok(ranking);
        }

        private bool isOwner(Guid userId)
        {
            var sub = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value; //get the value of sub claim
            if (sub == null) return false;
            if (Guid.Parse(sub) != userId)
            {
                return false;
            }
            return true;
        }
    }

}
