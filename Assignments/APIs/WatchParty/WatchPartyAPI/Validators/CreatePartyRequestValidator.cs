using FluentValidation;
using WatchPartyAPI.DTOs.Requests;
using WatchPartyAPI.Interfaces.Repositories;

namespace WatchPartyAPI.Validators
{
    public class CreatePartyRequestValidator : AbstractValidator<CreatePartyRequest>
    {
        public CreatePartyRequestValidator(IEpisodeRepository episodeRepository)
        {

            RuleFor(wp => wp.Title)
                .NotNull().WithMessage("Title required")
                .NotEmpty().WithMessage("Title required")
                .MinimumLength(5).WithMessage("Title requires at least 5 characters")
                .MaximumLength(100).WithMessage("Title must be at most 100 characters");

            RuleFor(wp => wp.CurrentEpisodeId)
                .NotNull().WithMessage("Episode ID is required")
                .NotEmpty().WithMessage("Episode ID is required");

            RuleSet("Async", () =>
            {
                RuleFor(wp => wp.CurrentEpisodeId)
                    .MustAsync(async (episodeId, ct) =>
                    {
                        return await episodeRepository.GetById(episodeId) != null;
                    }).WithMessage("The episode doesn't exists")
                    .When(wp => wp.CurrentEpisodeId != Guid.Empty);
            });
        }
    }
}
