using WatchTrackerAPI.Data;
using WatchTrackerAPI.DTOs;
using WatchTrackerAPI.Interfaces;
using WatchTrackerAPI.Models.Entities;
using WatchTrackerAPI.Models.Enums;

namespace WatchTrackerAPI.Services
{
    public class MediaProgressService : IMediaProgressService
    {
        private readonly AppDBContext _dbContext;
        private readonly IMediaService _mediaService;
        private readonly IUserService _userService;

        public MediaProgressService(AppDBContext dbContext, IMediaService mediaService, IUserService userService)
        {
            _dbContext = dbContext;
            _mediaService = mediaService;
            _userService = userService;
        }

        public MediaProgressResponse CreateOrUpdateProgress(CreateOrUpdateProgressRequest request, Guid userId)
        {
            var mediaId = request.MediaId;
            _userService.GetUser(userId);
            var media = _mediaService.GetMedia(mediaId);
            var exists = GetMediaProgress(userId, mediaId);
            UserMediaProgress progress;

            ValidatePersonalRating(request.PersonalRating, request.EpisodesWatched, request.WatchStatus);
            ValidateRatingValue(request.PersonalRating);
            if (exists == null) //if it doesn't exists
            {
                var newMedia = new UserMediaProgress
                {
                    UserId = userId,
                    MediaId = request.MediaId,
                    EpisodesWatched = request.EpisodesWatched,
                    Status = request.WatchStatus,
                    PersonalRating = request.PersonalRating,
                };
                _dbContext.MediaProgresses.Add(newMedia);
                newMedia.LastUpdatedAt = DateTime.UtcNow;
                newMedia.StartedAt = DateTime.UtcNow;
                if (request.WatchStatus == WatchStatus.Completed)
                {
                    newMedia.FinishedAt = DateTime.UtcNow;
                }
                progress = newMedia;
            }
            else //if it exists: update it
            {
                exists.EpisodesWatched = request.EpisodesWatched;
                exists.Status = request.WatchStatus;
                exists.PersonalRating = request.PersonalRating;
                exists.LastUpdatedAt = DateTime.UtcNow;
                if (request.WatchStatus == WatchStatus.Completed)
                {
                    exists.FinishedAt = DateTime.UtcNow;
                }
                if (exists.StartedAt == null)
                {
                    exists.StartedAt = DateTime.UtcNow;
                }
                progress = exists;
            }

            return new MediaProgressResponse
            {
                UserId = userId,
                MediaId = mediaId,
                MediaTitle = media.Title,
                MediaType = media.Type,
                TotalEpisodes = media.TotalEpisodes,
                EpisodesWatched = progress.EpisodesWatched,
                WatchStatus = progress.Status,
                PersonalRating = progress.PersonalRating,
                StartedAt = progress.StartedAt,
                FinishedAt = progress.FinishedAt,
                LastUpdatedAt = progress.LastUpdatedAt,
            };
        }

        public MediaProgressResponse UpdatePersonalRating(UpdatePersonalRatingRequest request, Guid userId, Guid mediaId)
        {
            _userService.GetUser(userId);
            var media = _mediaService.GetMedia(mediaId);
            var mediaProgress = GetMediaProgress(userId, mediaId);

            if (mediaProgress == null)
            {
                throw new InvalidOperationException("The media progress you try to update doesn't exists");
            }

            ValidatePersonalRating(request.PersonalRating, mediaProgress.EpisodesWatched, mediaProgress.Status);
            ValidateRatingValue(request.PersonalRating);
            mediaProgress.PersonalRating = request.PersonalRating;

            return new MediaProgressResponse
            {
                UserId = userId,
                MediaId = mediaId,
                MediaTitle = media.Title,
                MediaType = media.Type,
                TotalEpisodes = media.TotalEpisodes,
                EpisodesWatched = mediaProgress.EpisodesWatched,
                WatchStatus = mediaProgress.Status,
                PersonalRating = mediaProgress.PersonalRating,
                StartedAt = mediaProgress.StartedAt,
                FinishedAt = mediaProgress.FinishedAt,
                LastUpdatedAt = mediaProgress.LastUpdatedAt,
            };
        }

        public PagedResponse<MediaProgressResponse> GetAllUserProgress(int page, int pageSize, Guid userId, WatchStatus? status)
        {

            var allMediaProgress = _dbContext.MediaProgresses;
            allMediaProgress = allMediaProgress.Where(p => p.UserId == userId).ToList();

            if (status != null) //filter by status
            {
                allMediaProgress = allMediaProgress.Where(m => m.Status == status).ToList();
            }

            var totalCount = allMediaProgress.Count;
            allMediaProgress = allMediaProgress.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var items = allMediaProgress.Select(mediaProgess =>
            {
                var media = _mediaService.GetMedia(mediaProgess.MediaId);

                return new MediaProgressResponse
                {
                    UserId = userId,
                    MediaId = media.Id,
                    MediaTitle = media.Title,
                    MediaType = media.Type,
                    TotalEpisodes = media.TotalEpisodes,
                    EpisodesWatched = mediaProgess.EpisodesWatched,
                    WatchStatus = mediaProgess.Status,
                    PersonalRating = mediaProgess.PersonalRating,
                    StartedAt = mediaProgess.StartedAt,
                    FinishedAt = mediaProgess.FinishedAt,
                    LastUpdatedAt = mediaProgess.LastUpdatedAt
                };
            });

            var paginatedResponse = new PagedResponse<MediaProgressResponse>
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalCount = totalCount,
            };
            return paginatedResponse;
        }

        private UserMediaProgress? GetMediaProgress(Guid userId, Guid mediaId)
        {
            var mediaProgress = _dbContext.MediaProgresses.FirstOrDefault(p => p.UserId == userId && p.MediaId == mediaId);
            return mediaProgress;
        }

        private void ValidatePersonalRating(int? personalRating, int episodesWtached, WatchStatus status)
        {
            if (personalRating.HasValue && episodesWtached == 0 && status != WatchStatus.Completed)
            {
                throw new InvalidOperationException("You have to see at least 1 episode of the serie or the entire movie to insert a rating!");
            }
        }

        private void ValidateRatingValue(int? personalRating)
        {
            if (personalRating.HasValue && (personalRating < 1 || personalRating > 10))
            {
                throw new InvalidOperationException($"The {personalRating} value just inserted doesn't meet the allowed interval");
            }
        }
    }
}
