using WatchTrackerAPI.DTOs.Parameters;
using WatchTrackerAPI.DTOs.Responses;

namespace WatchTrackerAPI.Interfaces.Services
{
    public interface IGenreService
    {
        public Task<PagedResponse<GenreResponse>> GetAllGenres(PaginationParams genreParams);
    }
}
