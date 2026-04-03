using WatchPartyAPI.DTOs.Requests;
using WatchPartyAPI.DTOs.Responses;
using WatchPartyAPI.Exceptions;
using WatchPartyAPI.Interfaces.Repositories;
using WatchPartyAPI.Interfaces.Services;
using WatchPartyAPI.Models;
using WatchPartyAPI.Models.Enums;

namespace WatchPartyAPI.Services
{
    public class WatchPartyService : IWatchPartyService
    {
        private readonly IWatchPartyRepository _watchPartyRepository;
        private readonly IEpisodeService _episodeService;
        private readonly IParticipantService _participantService;
        private readonly ILogger<WatchPartyService> _logger;

        public WatchPartyService(IWatchPartyRepository watchPartyRepository, IEpisodeService episodeService, IParticipantService participantService, ILogger<WatchPartyService> logger)
        {
            _watchPartyRepository = watchPartyRepository;
            _episodeService = episodeService;
            _participantService = participantService;
            _logger = logger;
        }

        public async Task ChangeEpisode(Guid watchPartyId, Guid userId, ChangeEpisodeRequest request)
        {
            var party = await GetPartyEntity(watchPartyId);
            IsHost(userId, party);
            IsPartyEnded(party);
            await _episodeService.GetEpisode(request.EpisodeId);

            var previousEpisode = party.CurrentEpisodeId;
            party.CurrentEpisodeId = request.EpisodeId;
            party.Status = PartyStatus.Waiting;
            await _watchPartyRepository.Update(party);
            _logger.LogInformation(
                "WatchParty {PartyId} episode changed from {PreviousEpisode} to {NewEpisode} by host {HostUserId}",
                 watchPartyId, previousEpisode, request.EpisodeId, userId);
        }

        public async Task<WatchPartyResponse> CreateParty(CreatePartyRequest request, Guid hostUserId)
        {
            await _episodeService.GetEpisode(request.CurrentEpisodeId);

            var wp = new WatchParty
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                HostUserId = hostUserId,
                CurrentEpisodeId = request.CurrentEpisodeId,
                Status = PartyStatus.Waiting,
            };

            await _watchPartyRepository.Create(wp);
            await _participantService.CreateParticipant(hostUserId, wp.Id, ParticipantRole.Host);

            _logger.LogInformation(
                "WatchParty {PartyId} created by host {HostUserId} with episode {EpisodeId}",
                 wp.Id, hostUserId, wp.CurrentEpisodeId);

            return new WatchPartyResponse
            {
                Id = wp.Id,
                Title = wp.Title,
                HostUserId = hostUserId,
                CurrentEpisodeId = wp.CurrentEpisodeId,
                Status = wp.Status,
            };
        }

        public async Task EndParty(Guid watchPartyId, Guid userId)
        {
            var party = await GetPartyEntity(watchPartyId);
            IsHost(userId, party);
            IsPartyEnded(party);
            party.Status = PartyStatus.Ended;
            await _watchPartyRepository.Update(party);

            _logger.LogInformation(
                "WatchParty {PartyId} ended by host {HostUserId}",
                watchPartyId, userId);
        }

        public async Task<WatchPartyResponse> GetParty(Guid watchPartyId)
        {
            var wp = await GetPartyEntity(watchPartyId);
            return new WatchPartyResponse
            {
                Id = wp.Id,
                Title = wp.Title,
                HostUserId = wp.HostUserId,
                CurrentEpisodeId = wp.CurrentEpisodeId,
                Status = wp.Status,
            };
        }

        public async Task JoinParty(Guid watchPartyId, Guid userId)
        {
            await _participantService.CreateParticipant(userId, watchPartyId, ParticipantRole.Viewer);
            _logger.LogInformation(
                "User {UserId} joined WatchParty {PartyId}",
                 userId, watchPartyId);
        }

        public async Task UpdatePlayback(Guid watchPartyId, Guid userId, UpdatePlaybackRequest request)
        {
            var party = await GetPartyEntity(watchPartyId);
            IsHost(userId, party);
            IsPartyEnded(party);
            ValidateTransition(party.Status, request.Status);
            var previousStatus = party.Status;
            party.Status = request.Status;
            await _watchPartyRepository.Update(party);

            _logger.LogInformation(
                "WatchParty {PartyId} playback updated from {PreviousStatus} to {NewStatus} by host {HostUserId}",
                 watchPartyId, previousStatus, request.Status, userId);
        }

        private async Task<WatchParty> GetPartyEntity(Guid watchPartyId)
        {
            var wp = await _watchPartyRepository.GetById(watchPartyId);
            if (wp == null) throw new NotFoundException("WatchParty", watchPartyId);
            return wp;
        }

        private void IsHost(Guid userId, WatchParty party)
        {
            if (party.HostUserId != userId)
                throw new ForbiddenException("Only the host can perform this action");
        }

        private void IsPartyEnded(WatchParty party)
        {
            if (party.Status == PartyStatus.Ended)
                throw new BusinessException("The party has already ended");
        }

        private void ValidateTransition(PartyStatus current, PartyStatus requested)
        {
            var validTransitions = new Dictionary<PartyStatus, List<PartyStatus>>
            {
                { PartyStatus.Waiting, new List<PartyStatus> { PartyStatus.Playing } },
                { PartyStatus.Playing, new List<PartyStatus> { PartyStatus.Paused } },
                { PartyStatus.Paused,  new List<PartyStatus> { PartyStatus.Playing } },
                { PartyStatus.Ended,   new List<PartyStatus>() } //To nothing
            };

            if (!validTransitions[current].Contains(requested))
                throw new BusinessException($"Cannot transition from {current} to {requested}");
        }
    }
}