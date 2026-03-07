using Microsoft.AspNetCore.Mvc;
using WatchTrackerAPI.DTOs;
using WatchTrackerAPI.Interfaces;
using WatchTrackerAPI.Models.Enums;

namespace WatchTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MediaController : Controller
    {
        private readonly IMediaService _mediaService;
        public MediaController(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        [HttpGet]
        public IActionResult GetAllMedia([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] MediaTypes? type = null)
        {
            var media = _mediaService.GetAllMedia(page, pageSize, type);
            return Ok(media);
        }

        [HttpGet("{id}")]
        public IActionResult GetMedia(Guid id)
        {
            try
            {
                var media = _mediaService.GetMedia(id);
                return Ok(media);
            }
            catch (InvalidOperationException exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateMedia(CreateMediaRequest mediaToCreate)
        {
            try
            {
                var media = _mediaService.CreateMedia(mediaToCreate);
                return CreatedAtAction("GetMedia", new { id = media.Id }, media);
            }
            catch (InvalidOperationException exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
