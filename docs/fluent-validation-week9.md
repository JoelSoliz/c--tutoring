# EJERCICIO - FLUENT VALIDATION
Implementa los validators para estos DTOs de una API de películas románticas:

```
public class CreateMovieDto
{
	public string Title { get; set; } = string.Empty;
	public string Director { get; set; } = string.Empty;
	public int ReleaseYear { get; set; }
	public int DurationMinutes { get; set; }
	public string Genre { get; set; } = string.Empty;
	public double Rating { get; set; }
	public List<string> Actors { get; set; } = [];
}
public class CreateReviewDto
{
	public Guid MovieId { get; set; }
	public string Content { get; set; } = string.Empty;
	public int Score { get; set; }
	public bool IsSpoiler { get; set; }
	public string? SpoilerWarning { get; set; }
}
```
## Requisitos:
1. Title: requerido, entre 2 y 300 caracteres.
2. Director: requerido, máximo 150 caracteres.
3. ReleaseYear: entre 1888 y el año actual + 2.
4. DurationMinutes: entre 30 y 300.
5. Genre: debe ser uno de: Romance, Drama, Comedy.
6. Rating: entre 0.0 y 10.0.
7. Actors: al menos 1, máximo 20.
8. Content: requerido, entre 10 y 2000 caracteres.
9. Score: entre 1 y 5.
10. SpoilerWarning: requerido solo si IsSpoiler es true.

## CODE
```
public class CreateMovieValidator : AbstractValidator<CreateMovieDto>
{
	private static readonly string[] ValidGenres = ["Romance", "Drama", "Comedy"];

	public CreateMovieValidator()
	{
		RuleFor(movie => movie.Title)
			.NotEmpty().WithMessage("Title is required")
			.MinimumLength(30).WithMessage("Title must be at least 30 characters")
            .MaximumLength(300).WithMessage("Title must be at most 300 characters");

		RuleFor(movie => movie.Director)
			.NotEmpty().WithMessage("Director is required")
            .MaximumLength(150).WithMessage("Director must be at most 150 characters");

		RuleFor(movie => movie.ReleaseYear)
			.GreaterThan(1888).WithMessage("Release year must be greater than 1888")
			.LessThanOrEqualTo(DateTime.UtcNow.Year + 2)
			.WithMessage($"Release year cannot exceed {DateTime.UtcNow.Year + 2}");

		RuleFor(movie => movie.DurationMinutes)
			.GreaterThan(30).WithMessage("DurationMinutes must be greater than 30")
			.LessThanOrEqualTo(300).WithMessage("Duration cannot exceed 300 minutes");

		RuleFor(movie => movie.Genre)
            .NotEmpty().WithMessage("Genre is required")
            .Must(g => ValidGenres.Contains(g))
            .WithMessage($"Genre must be one of: {string.Join(", ", ValidGenres)}");

        RuleFor(movie => movie.Rating)
            .InclusiveBetween(0.0, 10.0).WithMessage("Rating must be between 0.0 and 10.0");

        RuleFor(movie => movie.Actors)
            .NotEmpty().WithMessage("At least 1 actor is required")
            .Must(actors => actors.Count <= 20)
            .WithMessage("Cannot have more than 20 actors");
	}
}
```

```
public class CreateReviewValidator : AbstractValidator<CreateReviewDto>
{
    public CreateReviewValidator()
    {
        RuleFor(review => review.Content)
            .NotEmpty().WithMessage("Content is required")
            .MinimumLength(10).WithMessage("Content must be at least 10 characters")
            .MaximumLength(2000).WithMessage("Content must be at most 2000 characters");

        RuleFor(review => review.Score)
            .InclusiveBetween(1, 5).WithMessage("Score must be between 1 and 5");

        RuleFor(review => review.SpoilerWarning)
            .NotEmpty().WithMessage("Spoiler warning is required when IsSpoiler is true")
            .When(review => review.IsSpoiler);
    }
}
```