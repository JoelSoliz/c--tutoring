using FluentValidation;
using WatchPartyAPI.DTOs.Requests;

namespace WatchPartyAPI.Validators
{
    public class CreateEpisodeRequestValidator : AbstractValidator<CreateEpisodeRequest>
    {
        public CreateEpisodeRequestValidator()
        {
            RuleFor(episode => episode.AnimeTitle)
                .NotEmpty().WithMessage("Anime title is required")
                .MinimumLength(5).WithMessage("Anime title must be at least 5 characters")
                .MaximumLength(255).WithMessage("Anime title must be at most 255 characters");

            RuleFor(episode => episode.EpisodeNumber)
                .GreaterThan(0).WithMessage("Episode number must be greater than 0")
                .LessThanOrEqualTo(1000).WithMessage("Episode number cannot exceed 1000");

            RuleFor(x => x.DurationSeconds)
                .GreaterThan(0).WithMessage("Duration must be greater than 0")
                .LessThanOrEqualTo(7200).WithMessage("Duration cannot exceed 7200 seconds (2 hours)");
        }
    }
}
