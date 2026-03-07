using WatchTrackerAPI.Data;
using WatchTrackerAPI.DTOs;
using WatchTrackerAPI.Interfaces;
using WatchTrackerAPI.Models.Entities;
using WatchTrackerAPI.Models.Enums;

namespace WatchTrackerAPI.Services
{
    public class MediaService : IMediaService
    {
        private readonly AppDBContext _dbContext;
        public MediaService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public MediaResponse CreateMedia(CreateMediaRequest request)
        {
            if ((request.Type == MediaTypes.Anime || request.Type == MediaTypes.TVShow) && (request.TotalEpisodes == null))
            {
                throw new InvalidOperationException($"The type {request.Type} needs a number of episodes");
            }
            var newMedia = new Media
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Type = request.Type,
                TotalEpisodes = request.TotalEpisodes,
                ReleaseDate = request.ReleaseDate,
                Genre = request.Genre,
                CreatedAt = DateTime.UtcNow,
            };

            _dbContext.MediaContent.Add(newMedia);
            return new MediaResponse
            {
                Id = newMedia.Id,
                Title = newMedia.Title,
                Type = newMedia.Type,
                TotalEpisodes = newMedia.TotalEpisodes,
                ReleaseDate = newMedia.ReleaseDate,
                Genre = newMedia.Genre,
                CreatedAt = newMedia.CreatedAt
            };
        }

        public PagedResponse<MediaResponse> GetAllMedia(int page, int pageSize, MediaTypes? type)
        {
            var allMedia = _dbContext.MediaContent;
            if (type != null) //filter by type
            {
                allMedia = allMedia.Where(m => m.Type == type).ToList();
            }

            var totalCount = allMedia.Count;
            allMedia = allMedia.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var items = allMedia.Select(media => new MediaResponse
            {
                Id = media.Id,
                Title = media.Title,
                Type = media.Type,
                TotalEpisodes = media.TotalEpisodes,
                ReleaseDate = media.ReleaseDate,
                Genre = media.Genre,
                CreatedAt = media.CreatedAt,
            });

            var paginatedResponse = new PagedResponse<MediaResponse>
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalCount = totalCount,
            };
            return paginatedResponse;
        }

        public MediaResponse GetMedia(Guid id)
        {
            var media = _dbContext.MediaContent.FirstOrDefault(media => media.Id == id);
            if (media == null)
            {
                throw new InvalidOperationException($"The Media with {id} was not found");
            }

            var response = new MediaResponse
            {
                Id = media.Id,
                Title = media.Title,
                Type = media.Type,
                TotalEpisodes = media.TotalEpisodes,
                ReleaseDate = media.ReleaseDate,
                Genre = media.Genre,
                CreatedAt = media.CreatedAt,
            };
            return response;
        }
    }
}
