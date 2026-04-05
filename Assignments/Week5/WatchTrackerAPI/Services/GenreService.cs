using WatchTrackerAPI.DTOs.Parameters;
using WatchTrackerAPI.DTOs.Responses;
using WatchTrackerAPI.Interfaces.Repositories;
using WatchTrackerAPI.Interfaces.Services;

namespace WatchTrackerAPI.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<PagedResponse<GenreResponse>> GetAllGenres(PaginationParams genreParams)
        {
            var genres = await _genreRepository.GetAllGenres(genreParams.Page, genreParams.PageSize);
            var totalPages = (int)Math.Ceiling((double)genres.TotalCount / genreParams.PageSize);

            var paginatedGenres = genres.Items.Select(genres => new GenreResponse
            {
                Id = genres.Id,
                Name = genres.Name,
            }).ToList();

            var paginatedResponse = new PagedResponse<GenreResponse>
            {
                Items = paginatedGenres,
                Page = genreParams.Page,
                PageSize = genreParams.PageSize,
                TotalPages = totalPages,
                TotalCount = genres.TotalCount
            };

            return paginatedResponse;
        }
    }
}
