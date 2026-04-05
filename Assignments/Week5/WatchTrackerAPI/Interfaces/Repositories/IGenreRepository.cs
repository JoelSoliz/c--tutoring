using WatchTrackerAPI.Models.Entities;

namespace WatchTrackerAPI.Interfaces.Repositories
{
    public interface IGenreRepository
    {
        public Task CreateGenre(Genre genre);
        public Task<Genre?> GetGenreByName(string name);

        public Task<(List<Genre> Items, int TotalCount)> GetAllGenres(int page, int pageSize);
    }
}
