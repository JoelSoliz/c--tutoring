using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using WatchTrackerAPI.DTOs.Parameters;
using WatchTrackerAPI.DTOs.Requests;
using WatchTrackerAPI.Interfaces.Services;

namespace WatchTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/users/{userId}/media-progress")]
    [Authorize(Roles = "User")]
    public class MediaProgressController : Controller
    {
        private readonly IMediaProgressService _mediaProgressService;

        public MediaProgressController(IMediaProgressService mediaProgressService)
        {
            _mediaProgressService = mediaProgressService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProgress(CreateProgressRequest request, [FromRoute] Guid userId)
        {
            if (!isOwner(userId)) return Forbid();
            try
            {
                var progress = await _mediaProgressService.CreateUserProgress(request, userId);
                return CreatedAtAction("GetUserMediaProgress", new { userId = progress.UserId, mediaId = progress.MediaId }, progress);
            }
            catch (InvalidOperationException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPatch("{mediaId}")]
        public async Task<IActionResult> UpdateUserMediaProgress(UpdateProgressRequest request, [FromRoute] Guid userId, [FromRoute] Guid mediaId)
        {
            if (!isOwner(userId)) return Forbid();
            try
            {
                var updatedProgress = await _mediaProgressService.UpdateUserProgress(request, userId, mediaId);
                return Ok(updatedProgress);
            }
            catch (InvalidOperationException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserMediaProgress([FromRoute] Guid userId, [FromQuery] MediaProgressQueryParams mediaProgressParams)
        {
            if (!isOwner(userId)) return Forbid();
            var userMediaProgress = await _mediaProgressService.GetAllUserProgress(userId, mediaProgressParams);
            return Ok(userMediaProgress);
        }

        [HttpGet("{mediaId}")]
        public async Task<IActionResult> GetUserMediaProgress([FromRoute] Guid userId, [FromRoute] Guid mediaId)
        {
            if (!isOwner(userId)) return Forbid();
            try
            {
                var progress = await _mediaProgressService.GetUserProgress(userId, mediaId);
                return Ok(progress);
            }
            catch (InvalidOperationException exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpGet("watchlist")]
        public async Task<IActionResult> GetRomanticWatchList([FromRoute] Guid userId, [FromQuery] WatchlistQueryParams watchlistParams)
        {
            if (!isOwner(userId)) return Forbid();
            var romanticList = await _mediaProgressService.GetRomanticWatchList(userId, watchlistParams);
            return Ok(romanticList);
        }

        [HttpGet("recent-activity")]
        public async Task<IActionResult> GetRecentActivity([FromRoute] Guid userId, [FromQuery] RecentActivityQueryParams recentActivityParams)
        {
            if (!isOwner(userId)) return Forbid();
            var activity = await _mediaProgressService.GetRecentActivity(userId, recentActivityParams);
            return Ok(activity);
        }

        [HttpDelete("{mediaId}")]
        public async Task<IActionResult> DeleteUserProgress([FromRoute] Guid userId, [FromRoute] Guid mediaId)
        {
            if (!isOwner(userId)) return Forbid();
            try
            {
                await _mediaProgressService.DeleteUserProgress(userId, mediaId);
                return NoContent();
            }
            catch (InvalidOperationException exception)
            {
                return NotFound(exception.Message);
            }
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
