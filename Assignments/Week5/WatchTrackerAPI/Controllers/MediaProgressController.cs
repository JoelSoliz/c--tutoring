using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchTrackerAPI.DTOs.Parameters;
using WatchTrackerAPI.DTOs.Requests;
using WatchTrackerAPI.Interfaces.Services;

namespace WatchTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/users/media-progress")]
    [Authorize(Roles = "User")]
    public class MediaProgressController : BaseController
    {
        private readonly IMediaProgressService _mediaProgressService;

        public MediaProgressController(IMediaProgressService mediaProgressService)
        {
            _mediaProgressService = mediaProgressService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProgress(CreateProgressRequest request)
        {
            var userId = GetUserIdFromToken();
            if (userId == null) return Unauthorized();
            try
            {
                var progress = await _mediaProgressService.CreateUserProgress(request, userId.Value);
                return CreatedAtAction("GetUserMediaProgress", new { userId = progress.UserId, mediaId = progress.MediaId }, progress);
            }
            catch (InvalidOperationException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPatch("{mediaId}")]
        public async Task<IActionResult> UpdateUserMediaProgress(UpdateProgressRequest request, [FromRoute] Guid mediaId)
        {
            var userId = GetUserIdFromToken();
            if (userId == null) return Unauthorized();
            try
            {
                var updatedProgress = await _mediaProgressService.UpdateUserProgress(request, userId.Value, mediaId);
                return Ok(updatedProgress);
            }
            catch (InvalidOperationException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserMediaProgress([FromQuery] MediaProgressQueryParams mediaProgressParams)
        {
            var userId = GetUserIdFromToken();
            if (userId == null) return Unauthorized();
            var userMediaProgress = await _mediaProgressService.GetAllUserProgress(userId.Value, mediaProgressParams);
            return Ok(userMediaProgress);
        }

        [HttpGet("{mediaId}")]
        public async Task<IActionResult> GetUserMediaProgress([FromRoute] Guid mediaId)
        {
            var userId = GetUserIdFromToken();
            if (userId == null) return Unauthorized();
            try
            {
                var progress = await _mediaProgressService.GetUserProgress(userId.Value, mediaId);
                return Ok(progress);
            }
            catch (InvalidOperationException exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpGet("watchlist")]
        public async Task<IActionResult> GetRomanticWatchList([FromQuery] WatchlistQueryParams watchlistParams)
        {
            var userId = GetUserIdFromToken();
            if (userId == null) return Unauthorized();
            var romanticList = await _mediaProgressService.GetRomanticWatchList(userId.Value, watchlistParams);
            return Ok(romanticList);
        }

        [HttpGet("recent-activity")]
        public async Task<IActionResult> GetRecentActivity([FromQuery] RecentActivityQueryParams recentActivityParams)
        {
            var userId = GetUserIdFromToken();
            if (userId == null) return Unauthorized();
            var activity = await _mediaProgressService.GetRecentActivity(userId.Value, recentActivityParams);
            return Ok(activity);
        }

        [HttpDelete("{mediaId}")]
        public async Task<IActionResult> DeleteUserProgress([FromRoute] Guid mediaId)
        {
            var userId = GetUserIdFromToken();
            if (userId == null) return Unauthorized();
            try
            {
                await _mediaProgressService.DeleteUserProgress(userId.Value, mediaId);
                return NoContent();
            }
            catch (InvalidOperationException exception)
            {
                return NotFound(exception.Message);
            }
        }
    }
}
