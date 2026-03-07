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

        public MediaProgressService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public MediaProgressResponse CreateOrUpdateProgress(CreateOrUpdateProgressRequest request, Guid userId)
        {
            var mediaId = request.MediaId;
            ValidateUser(userId);
            var media = ValidateMedia(mediaId);
            var isExisting = GetMediaProgress(userId, mediaId);
            UserMediaProgress progress;

            ValidatePersonalRating(request.PersonalRating, request.EpisodesWatched, request.WatchStatus);
            ValidateRatingValue(request.PersonalRating);
            if (isExisting == null) //if it doesn't exists
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
                isExisting.EpisodesWatched = request.EpisodesWatched;
                isExisting.Status = request.WatchStatus;
                isExisting.PersonalRating = request.PersonalRating;
                isExisting.LastUpdatedAt = DateTime.UtcNow;
                if (request.WatchStatus == WatchStatus.Completed)
                {
                    isExisting.FinishedAt = DateTime.UtcNow;
                }
                if (isExisting.StartedAt == null)
                {
                    isExisting.StartedAt = DateTime.UtcNow;
                }
                progress = isExisting;
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
            ValidateUser(userId);
            var media = ValidateMedia(mediaId);
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
                var media = ValidateMedia(mediaProgess.MediaId);

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

        private User ValidateUser(Guid userId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                throw new InvalidOperationException("The User doesn't exists");
            }
            return user;
        }

        private Media ValidateMedia(Guid mediaId)
        {
            var media = _dbContext.MediaContent.FirstOrDefault(m => m.Id == mediaId);
            if (media == null)
            {
                throw new InvalidOperationException("The Media doesn't exists");
            }
            return media;
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
