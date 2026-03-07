using WatchTrackerAPI.Models.Entities;
using WatchTrackerAPI.Models.Enums;

namespace WatchTrackerAPI.Data
{
    public class AppDBContext
    {
        public AppDBContext()
        {
            MediaContent = new List<Media> {
                new Media ()
                {
                    Id = Guid.NewGuid(),
                    Title = "Bridgerton",
                    Type = MediaTypes.TVShow,
                    TotalEpisodes = 32,
                    ReleaseDate = new DateTime(2026,2,26, 15,50,00),
                    Genre = "Romantic Regency"
                }
            };

            Users = new List<User> { };
            MediaProgresses = new List<UserMediaProgress> { };
        }

        public List<Media> MediaContent;
        public List<User> Users;
        public List<UserMediaProgress> MediaProgresses;
    }
}
