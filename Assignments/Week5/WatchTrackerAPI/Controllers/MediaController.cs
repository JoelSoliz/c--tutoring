using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchTrackerAPI.DTOs.Parameters;
using WatchTrackerAPI.DTOs.Requests;
using WatchTrackerAPI.Interfaces.Services;

namespace WatchTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Moderator")]
    public class MediaController : Controller
    {
        private readonly IMediaService _mediaService;
        public MediaController(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllMedia([FromQuery] MediaQueryParams mediaParams)
        {
            var media = await _mediaService.GetAllMedia(mediaParams);
            return Ok(media);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedia(Guid id)
        {
            try
            {
                var media = await _mediaService.GetMedia(id);
                return Ok(media);
            }
            catch (InvalidOperationException exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateMedia(CreateMediaRequest mediaToCreate)
        {
            try
            {
                var media = await _mediaService.CreateMedia(mediaToCreate);
                return CreatedAtAction("GetMedia", new { id = media.Id }, media);
            }
            catch (InvalidOperationException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{mediaId}")]
        public async Task<IActionResult> DeleteMedia(Guid mediaId)
        {
            try
            {
                await _mediaService.DeleteMedia(mediaId);
                return NoContent();
            }
            catch (InvalidOperationException exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
