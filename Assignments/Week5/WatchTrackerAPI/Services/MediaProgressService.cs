using WatchTrackerAPI.DTOs;
using WatchTrackerAPI.Interfaces.Repositories;
using WatchTrackerAPI.Interfaces.Services;
using WatchTrackerAPI.Models.Entities;
using WatchTrackerAPI.Models.Enums;

namespace WatchTrackerAPI.Services
{
    public class MediaProgressService : IMediaProgressService
    {
        private readonly IMediaProgressRepository _mediaProgressRepository;
        private readonly IMediaRepository _mediaRepository;
        private readonly IMediaService _mediaService;
        private readonly IUserService _userService;

        public MediaProgressService(IMediaProgressRepository mediaProgressRepository, IMediaRepository mediaRepository, IMediaService mediaService, IUserService userService)
        {
            _mediaProgressRepository = mediaProgressRepository;
            _mediaRepository = mediaRepository;
            _mediaService = mediaService;
            _userService = userService;
        }

        public async Task<MediaProgressResponse> CreateUserProgress(CreateProgressRequest request, Guid userId)
        {
            await _userService.GetUser(userId);
            var media = await _mediaService.GetMedia(request.MediaId);

            ValidatePersonalRating(request.PersonalRating, request.EpisodesWatched, request.WatchStatus);
            ValidateRatingValue(request.PersonalRating);
            ValidateWatchedEpisodes(request.EpisodesWatched, media.TotalEpisodes);

            var newMedia = new UserMediaProgress
            {
                UserId = userId,
                MediaId = request.MediaId,
                EpisodesWatched = request.EpisodesWatched,
                Status = request.WatchStatus,
                PersonalRating = request.PersonalRating,
            };

            newMedia.LastUpdatedAt = DateTime.UtcNow;
            newMedia.StartedAt = DateTime.UtcNow;
            newMedia.FinishedAt = SetFinishedDate(request.WatchStatus);

            await _mediaProgressRepository.CreateMediaProgress(newMedia);

            return new MediaProgressResponse
            {
                UserId = userId,
                MediaId = newMedia.MediaId,
                MediaTitle = media.Title,
                MediaType = media.Type,
                Genre = media.Genre.Name,
                TotalEpisodes = media.TotalEpisodes,
                EpisodesWatched = newMedia.EpisodesWatched,
                WatchStatus = newMedia.Status,
                PersonalRating = newMedia.PersonalRating,
                StartedAt = newMedia.StartedAt,
                FinishedAt = newMedia.FinishedAt,
                LastUpdatedAt = newMedia.LastUpdatedAt,
            };
        }

        public async Task<MediaProgressResponse> UpdateUserProgress(UpdateProgressRequest request, Guid userId, Guid mediaId)
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
            ValidateWatchedEpisodes(request.EpisodesWatched, media.TotalEpisodes);

            mediaProgress.EpisodesWatched = request.EpisodesWatched;
            mediaProgress.Status = request.WatchStatus;
            mediaProgress.PersonalRating = request.PersonalRating;
            mediaProgress.LastUpdatedAt = DateTime.UtcNow;
            mediaProgress.FinishedAt = SetFinishedDate(request.WatchStatus);

            await _mediaProgressRepository.UpdateMediaProgress(mediaProgress);

            return new MediaProgressResponse
            {
                UserId = userId,
                MediaId = mediaId,
                MediaTitle = media.Title,
                MediaType = media.Type,
                Genre = media.Genre.Name,
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

            var allMediaProgress = await _mediaProgressRepository.GetAllMediaProgress(mediaProgressParams.Page, mediaProgressParams.PageSize, mediaProgressParams.Status, userId);
            var totalPages = (int)Math.Ceiling((double)allMediaProgress.TotalCount / mediaProgressParams.PageSize);

            var mediaIds = allMediaProgress.Items.Select(progress => progress.MediaId).ToList(); //all media ids
            var medias = await _mediaRepository.GetMediaByIds(mediaIds);

            var items = allMediaProgress.Items.Select(mediaProgess =>
            {
                var media = medias.First(m => m.Id == mediaProgess.MediaId);

                return new MediaProgressResponse
                {
                    UserId = userId,
                    MediaId = media.Id,
                    MediaTitle = media.Title,
                    MediaType = media.Type,
                    Genre = media.Genre.Name,
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
                TotalCount = allMediaProgress.TotalCount,
            };
            return paginatedResponse;
        }

        public async Task<MediaProgressResponse> GetUserProgress(Guid userId, Guid mediaId)
        {
            var mediaProgress = await _mediaProgressRepository.GetMediaProgress(userId, mediaId);
            if (mediaProgress == null)
            {
                throw new InvalidOperationException("Media progress not found");
            }
            var media = await _mediaService.GetMedia(mediaId);
            var response = new MediaProgressResponse
            {
                UserId = userId,
                MediaId = mediaId,
                MediaTitle = media.Title,
                MediaType = media.Type,
                Genre = media.Genre.Name,
                TotalEpisodes = media.TotalEpisodes,
                EpisodesWatched = mediaProgress.EpisodesWatched,
                WatchStatus = mediaProgress.Status,
                PersonalRating = mediaProgress.PersonalRating,
                StartedAt = mediaProgress.StartedAt,
                FinishedAt = mediaProgress.FinishedAt,
                LastUpdatedAt = mediaProgress.LastUpdatedAt

            };

            return response;
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
                exists.IsDeleted = true;
                await _mediaProgressRepository.UpdateMediaProgress(exists); // save the new row state
            }
        }

        private async Task<UserMediaProgress?> GetMediaProgress(Guid userId, Guid mediaId)
        {
            var mediaProgress = await _mediaProgressRepository.GetMediaProgress(userId, mediaId);
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

        private DateTime? SetFinishedDate(WatchStatus status)
        {
            if (status == WatchStatus.Completed)
            {
                return DateTime.UtcNow;
            }
            return null;
        }

        private void ValidateWatchedEpisodes(int episodesWatched, int? totalEpisodes)
        {
            if (totalEpisodes != null && episodesWatched > totalEpisodes)
            {
                throw new InvalidOperationException($"The serie/anime has only {totalEpisodes} episodes, make sure to insert valid data");
            }

        }
    }
}
