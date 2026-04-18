using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchPartyAPI.DTOs.Params;
using WatchPartyAPI.DTOs.Requests;
using WatchPartyAPI.Interfaces.Services;

namespace WatchPartyAPI.Controllers
{
    [ApiController]
    [Route("api/watch-parties")]
    [Authorize]
    public class WatchPartyController : BaseController
    {
        private readonly IWatchPartyService _watchPartyService;

        public WatchPartyController(IWatchPartyService watchPartyService)
        {
            _watchPartyService = watchPartyService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateParty(CreatePartyRequest request, IValidator<CreatePartyRequest> validator)
        {
            var syncResult = await validator.ValidateAsync(request, options =>
            {
                options.IncludeRuleSets("default");
            });

            if (!syncResult.IsValid)
            {
                syncResult.AddToModelState(ModelState, null);
                return ValidationProblem(ModelState);
            }

            var asyncResult = await validator.ValidateAsync(request, options =>
            {
                options.IncludeRuleSets("Async");
            });

            if (!asyncResult.IsValid)
            {
                asyncResult.AddToModelState(ModelState, null);
                return ValidationProblem(ModelState);
            }

            var userId = GetUserIdFromToken();
            var party = await _watchPartyService.CreateParty(request, userId);
            return CreatedAtAction(nameof(GetParty), new { id = party.Id }, party);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetParty(Guid id)
        {
            var party = await _watchPartyService.GetParty(id);
            return Ok(party);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllParties([FromQuery] PaginationParams wpParams)
        {
            var parties = await _watchPartyService.GetAllParties(wpParams.Limit, wpParams.Page);
            return Ok(parties);
        }

        [HttpPost("{id}/participants")]
        public async Task<IActionResult> JoinParty(Guid id)
        {
            var userId = GetUserIdFromToken();
            await _watchPartyService.JoinParty(id, userId);
            return Ok();
        }

        [HttpPatch("{id}/playback")]
        public async Task<IActionResult> UpdatePlayback(Guid id, UpdatePlaybackRequest request)
        {
            var userId = GetUserIdFromToken();
            await _watchPartyService.UpdatePlayback(id, userId, request);
            return Ok();
        }

        [HttpPatch("{id}/episode")]
        public async Task<IActionResult> ChangeEpisode(Guid id, ChangeEpisodeRequest request, IValidator<ChangeEpisodeRequest> validator)
        {
            var syncResult = await validator.ValidateAsync(request, options =>
                options.IncludeRuleSets("default"));

            if (!syncResult.IsValid)
            {
                syncResult.AddToModelState(ModelState, null);
                return ValidationProblem(ModelState);
            }

            var asyncResult = await validator.ValidateAsync(request, options =>
                options.IncludeRuleSets("Async"));

            if (!asyncResult.IsValid)
            {
                asyncResult.AddToModelState(ModelState, null);
                return ValidationProblem(ModelState);
            }

            var userId = GetUserIdFromToken();
            await _watchPartyService.ChangeEpisode(id, userId, request);
            return Ok();
        }

        [HttpPost("{id}/end")]
        public async Task<IActionResult> EndParty(Guid id)
        {
            var userId = GetUserIdFromToken();
            await _watchPartyService.EndParty(id, userId);
            return Ok();
        }
    }
}
