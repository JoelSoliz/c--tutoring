using WatchTrackerAPI.DTOs.Parameters;
using WatchTrackerAPI.DTOs.Requests;
using WatchTrackerAPI.DTOs.Responses;
using WatchTrackerAPI.Interfaces.Repositories;
using WatchTrackerAPI.Interfaces.Services;
using WatchTrackerAPI.Models.Entities;
using WatchTrackerAPI.Models.Enums;

namespace WatchTrackerAPI.Services
{
    public class MediaService : IMediaService
    {
        private readonly IMediaRepository _mediaRepository;
        private readonly IGenreRepository _genreRepository;
        public MediaService(IMediaRepository mediaRepository, IGenreRepository genreRepository)
        {
            _mediaRepository = mediaRepository;
            _genreRepository = genreRepository;
        }

        public async Task<MediaResponse> CreateMedia(CreateMediaRequest request)
        {
            if ((request.Type == MediaTypes.Anime || request.Type == MediaTypes.TVShow) && (request.TotalEpisodes == null)) //tv show and anime are series
            {
                throw new InvalidOperationException($"The type {request.Type} needs a number of episodes");
            }

            var normalizedGenre = char.ToUpper(request.Genre[0]) + request.Genre.Substring(1).ToLower();
            var genre = await _genreRepository.GetGenreByName(normalizedGenre);
            if (genre == null)
            {
                genre = new Genre { Id = Guid.NewGuid(), Name = normalizedGenre };
                await _genreRepository.CreateGenre(genre);
            }

            var newMedia = new Media
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Type = request.Type,
                TotalEpisodes = request.TotalEpisodes,
                ReleaseDate = request.ReleaseDate,
                GenreId = genre.Id,
                CreatedAt = DateTime.UtcNow,
            };

            await _mediaRepository.CreateMedia(newMedia);

            return new MediaResponse
            {
                Id = newMedia.Id,
                Title = newMedia.Title,
                Type = newMedia.Type,
                TotalEpisodes = newMedia.TotalEpisodes,
                ReleaseDate = newMedia.ReleaseDate,
                Genre = new GenreResponse { Id = genre.Id, Name = genre.Name },
                CreatedAt = newMedia.CreatedAt
            };
        }

        public async Task<PagedResponse<MediaResponse>> GetAllMedia(MediaQueryParams mediaParams)
        {
            var paginatedMedia = await _mediaRepository.GetAllMedia(mediaParams.Page, mediaParams.PageSize, mediaParams.Type);
            var totalPages = (int)Math.Ceiling((double)paginatedMedia.TotalCount / mediaParams.PageSize);

            var items = paginatedMedia.Items.Select(media => new MediaResponse
            {
                Id = media.Id,
                Title = media.Title,
                Type = media.Type,
                TotalEpisodes = media.TotalEpisodes,
                ReleaseDate = media.ReleaseDate,
                Genre = new GenreResponse { Id = media.Genre.Id, Name = media.Genre.Name },
                CreatedAt = media.CreatedAt,
            }).ToList();

            var paginatedResponse = new PagedResponse<MediaResponse>
            {
                Items = items,
                Page = mediaParams.Page,
                PageSize = mediaParams.PageSize,
                TotalPages = totalPages,
                TotalCount = paginatedMedia.TotalCount
            };
            return paginatedResponse;
        }

        public async Task<MediaResponse> GetMedia(Guid id)
        {
            var media = await FindValidMedia(id);

            var response = new MediaResponse
            {
                Id = media.Id,
                Title = media.Title,
                Type = media.Type,
                TotalEpisodes = media.TotalEpisodes,
                ReleaseDate = media.ReleaseDate,
                Genre = new GenreResponse { Id = media.Genre.Id, Name = media.Genre.Name },
                CreatedAt = media.CreatedAt,
            };
            return response;
        }

        public async Task DeleteMedia(Guid id)
        {
            var exists = await FindValidMedia(id);
            exists.IsDeleted = true;
            await _mediaRepository.UpdateMedia(exists);
        }

        private async Task<Media> FindValidMedia(Guid mediaId)
        {
            var media = await _mediaRepository.GetMedia(mediaId);
            if (media == null)
            {
                throw new InvalidOperationException($"The Media with {mediaId} was not found");
            }
            return media;
        }
    }
}
