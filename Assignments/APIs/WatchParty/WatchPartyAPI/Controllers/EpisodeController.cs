using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchPartyAPI.DTOs.Requests;
using WatchPartyAPI.Interfaces.Services;

namespace WatchPartyAPI.Controllers
{
    [ApiController]
    [Route("api/episodes")]
    [Authorize]
    public class EpisodeController : Controller
    {
        private readonly IEpisodeService _episodeService;
        public EpisodeController(IEpisodeService episodeService)
        {
            _episodeService = episodeService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateEpisode(CreateEpisodeRequest request)
        {
            var episode = await _episodeService.CreateEpisode(request);
            return CreatedAtAction(nameof(GetEpisode), new { id = episode.Id }, episode);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEpisode(Guid id)
        {
            var episode = await _episodeService.GetEpisode(id);
            return Ok(episode);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEpisodes(int limit = 5, int page = 1)
        {
            var episodes = await _episodeService.GetEpisodes(limit, page);
            return Ok(episodes);
        }
    }
}
