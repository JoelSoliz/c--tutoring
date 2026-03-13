using Microsoft.EntityFrameworkCore;
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

        public async Task<MediaResponse> CreateMedia(CreateMediaRequest request)
        {
            if ((request.Type == MediaTypes.Anime || request.Type == MediaTypes.TVShow) && (request.TotalEpisodes == null)) //tv show and anime are series
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
            await _dbContext.SaveChangesAsync();

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

        public async Task<PagedResponse<MediaResponse>> GetAllMedia(MediaQueryParams mediaParams)
        {
            IQueryable<Media> query = _dbContext.MediaContent;
            if (mediaParams.Type != null) //filter by type
            {
                query = query.Where(m => m.Type == mediaParams.Type);
            }
            var totalCount = await query.CountAsync();
            query = query.Skip((mediaParams.Page - 1) * mediaParams.PageSize).Take(mediaParams.PageSize);
            var totalPages = (int)Math.Ceiling((double)totalCount / mediaParams.PageSize);

            var items = await query.Select(media => new MediaResponse
            {
                Id = media.Id,
                Title = media.Title,
                Type = media.Type,
                TotalEpisodes = media.TotalEpisodes,
                ReleaseDate = media.ReleaseDate,
                Genre = media.Genre,
                CreatedAt = media.CreatedAt,
            }).ToListAsync();

            var paginatedResponse = new PagedResponse<MediaResponse>
            {
                Items = items,
                Page = mediaParams.Page,
                PageSize = mediaParams.PageSize,
                TotalPages = totalPages,
                TotalCount = totalCount,
            };
            return paginatedResponse;
        }

        public async Task<MediaResponse> GetMedia(Guid id)
        {
            var media = await _dbContext.MediaContent.FirstOrDefaultAsync(media => media.Id == id);
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

        public async Task DeleteMedia(Guid id)
        {
            var media = await FindValidMedia(id);
            _dbContext.MediaContent.Remove(media);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<Media> FindValidMedia(Guid mediaId)
        {
            var media = await _dbContext.MediaContent.FirstOrDefaultAsync(media => media.Id == mediaId);
            if (media == null)
            {
                throw new InvalidOperationException($"The Media with {mediaId} was not found");
            }
            return media;
        }
    }
}
