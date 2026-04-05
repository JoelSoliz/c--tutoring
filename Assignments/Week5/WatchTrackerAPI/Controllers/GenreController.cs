using Microsoft.AspNetCore.Mvc;
using WatchTrackerAPI.DTOs.Parameters;
using WatchTrackerAPI.Interfaces.Services;

namespace WatchTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenres([FromQuery] PaginationParams genreParams)
        {
            var genres = await _genreService.GetAllGenres(genreParams);
            return Ok(genres);
        }
    }
}
