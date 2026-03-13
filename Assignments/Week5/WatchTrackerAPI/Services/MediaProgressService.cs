using Microsoft.EntityFrameworkCore;
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

        public async Task<MediaProgressResponse> CreateOrUpdateProgress(CreateOrUpdateProgressRequest request, Guid userId)
        {
            var mediaId = request.MediaId;
            await _userService.GetUser(userId);
            var media = await _mediaService.GetMedia(mediaId);
            var exists = await GetMediaProgress(userId, mediaId);
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
                await _dbContext.SaveChangesAsync();

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

        public async Task<MediaProgressResponse> UpdatePersonalRating(UpdatePersonalRatingRequest request, Guid userId, Guid mediaId)
        {
            await _userService.GetUser(userId);
            var media = await _mediaService.GetMedia(mediaId);
            var mediaProgress = await GetMediaProgress(userId, mediaId);

            if (mediaProgress == null)
            {
                throw new InvalidOperationException("The media progress you try to update doesn't exists");
            }

            ValidatePersonalRating(request.PersonalRating, mediaProgress.EpisodesWatched, mediaProgress.Status);
            ValidateRatingValue(request.PersonalRating);
            mediaProgress.PersonalRating = request.PersonalRating;

            await _dbContext.SaveChangesAsync();

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

        public async Task<PagedResponse<MediaProgressResponse>> GetAllUserProgress(Guid userId, MediaProgressQueryParams mediaProgressParams)
        {

            IQueryable<UserMediaProgress> allMediaProgress = _dbContext.MediaProgresses;
            allMediaProgress = allMediaProgress.Where(p => p.UserId == userId && p.isDeleted == false); //searching progresses

            if (mediaProgressParams.Status != null) //filter by status
            {
                allMediaProgress = allMediaProgress.Where(m => m.Status == mediaProgressParams.Status);
            }

            var totalCount = await allMediaProgress.CountAsync();
            allMediaProgress = allMediaProgress.Skip((mediaProgressParams.Page - 1) * mediaProgressParams.PageSize).Take(mediaProgressParams.PageSize);
            var totalPages = (int)Math.Ceiling((double)totalCount / mediaProgressParams.PageSize);

            var progresses = await allMediaProgress.ToListAsync();
            var mediaIds = progresses.Select(progress => progress.MediaId).ToList(); //all media ids
            var medias = await _dbContext.MediaContent.Where(media => mediaIds.Contains(media.Id)).ToListAsync(); // WHERE mediaId IN mediaIds

            var items = progresses.Select(mediaProgess =>
            {
                var media = medias.First(m => m.Id == mediaProgess.MediaId);

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
            }).ToList();

            var paginatedResponse = new PagedResponse<MediaProgressResponse>
            {
                Items = items,
                Page = mediaProgressParams.Page,
                PageSize = mediaProgressParams.PageSize,
                TotalPages = totalPages,
                TotalCount = totalCount,
            };
            return paginatedResponse;
        }

        public async Task DeleteUserProgress(Guid userId, Guid mediaId) //if user wants to delete a movie, show, etc
        {
            var exists = await GetMediaProgress(userId, mediaId);
            if (exists == null)
            {
                throw new InvalidOperationException("The media progress doesn't exists");
            }
            else
            {
                exists.isDeleted = true;
                await _dbContext.SaveChangesAsync(); // save the new row state
            }
        }

        private async Task<UserMediaProgress?> GetMediaProgress(Guid userId, Guid mediaId)
        {
            var mediaProgress = await _dbContext.MediaProgresses.FirstOrDefaultAsync(p => p.UserId == userId && p.MediaId == mediaId && p.isDeleted == false);
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
