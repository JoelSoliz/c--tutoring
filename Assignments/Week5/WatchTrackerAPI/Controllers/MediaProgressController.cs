using Microsoft.AspNetCore.Mvc;
using WatchTrackerAPI.DTOs;
using WatchTrackerAPI.Interfaces.Services;

namespace WatchTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/users/{userId}/media-progress")]
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
            var userMediaProgress = await _mediaProgressService.GetAllUserProgress(userId, mediaProgressParams);
            return Ok(userMediaProgress);
        }

        [HttpGet("{mediaId}")]
        public async Task<IActionResult> GetUserMediaProgress([FromRoute] Guid userId, [FromRoute] Guid mediaId)
        {
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

        [HttpDelete("{mediaId}")]
        public async Task<IActionResult> DeleteUserProgress([FromRoute] Guid userId, [FromRoute] Guid mediaId)
        {
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
    }
}
