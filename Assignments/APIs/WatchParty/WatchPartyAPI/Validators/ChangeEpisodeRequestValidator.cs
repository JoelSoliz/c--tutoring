using FluentValidation;
using WatchPartyAPI.DTOs.Requests;
using WatchPartyAPI.Interfaces.Repositories;

namespace WatchPartyAPI.Validators
{
    public class ChangeEpisodeRequestValidator : AbstractValidator<ChangeEpisodeRequest>
    {
        public ChangeEpisodeRequestValidator(IEpisodeRepository episodeRepository)
        {
            RuleFor(episode => episode.EpisodeId)
                .NotEmpty().WithMessage("Episode ID is required");

            RuleSet("Async", () =>
            {
                RuleFor(episode => episode.EpisodeId)
                    .MustAsync(async (episodeId, ct) =>
                    {
                        return await episodeRepository.GetById(episodeId) != null;
                    })
                    .WithMessage("The episode does not exist")
                    .When(episode => episode.EpisodeId != Guid.Empty);
            });
        }
    }
}
