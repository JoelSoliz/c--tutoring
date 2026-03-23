using Microsoft.EntityFrameworkCore;
using WatchTrackerAPI.Data;
using WatchTrackerAPI.Interfaces.Repositories;
using WatchTrackerAPI.Models.Entities;

namespace WatchTrackerAPI.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly AppDBContext _dbContext;
        public GenreRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateGenre(Genre genre)
        {
            _dbContext.Genres.Add(genre);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Genre?> GetGenreByName(string name)
        {
            IQueryable<Genre> genres = _dbContext.Genres;
            var genre = await genres.Where(genre => genre.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();
            return genre;
        }
    }
}
