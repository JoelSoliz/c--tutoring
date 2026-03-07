using Microsoft.AspNetCore.Mvc;
using WatchTrackerAPI.DTOs;
using WatchTrackerAPI.Interfaces;
using WatchTrackerAPI.Models.Enums;

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
        public IActionResult CreateProgress(CreateOrUpdateProgressRequest request, [FromRoute] Guid userId)
        {
            try
            {
                var progress = _mediaProgressService.CreateOrUpdateProgress(request, userId);
                return Ok(progress);
            }
            catch (InvalidOperationException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPatch("{mediaId}/rating")]
        public IActionResult UpdatePersonalRating(UpdatePersonalRatingRequest request, [FromRoute] Guid userId, [FromRoute] Guid mediaId)
        {
            try
            {
                var updatedProgress = _mediaProgressService.UpdatePersonalRating(request, userId, mediaId);
                return Ok(updatedProgress);
            }
            catch (InvalidOperationException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAllUserMediaProgress([FromRoute] Guid userId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] WatchStatus? status = null)
        {
            var userMediaProgress = _mediaProgressService.GetAllUserProgress(page, pageSize, userId, status);
            return Ok(userMediaProgress);
        }

    }
}
