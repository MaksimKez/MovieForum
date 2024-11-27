using FluentValidation;
using MovieForum.BusinessLogic.Models;
using MovieForum.Data.Entities.Enums;
using static System.DateTime;

namespace MovieForum.BusinessLogic.Validators;

public class MovieValidator : AbstractValidator<Movie>
{
    public MovieValidator()
    {
        RuleFor(m => m.Title).NotEmpty().WithMessage("Title is required.")
            .MaximumLength(50).WithMessage("Title is too long.");
        
        RuleFor(m => m.ShortDescription).NotEmpty().WithMessage("Short description is required.")
            .MaximumLength(150).WithMessage("Short description is too long.");
        
        RuleFor(m => m.Description).NotEmpty().WithMessage("Description is required.")
            .MaximumLength(3000).WithMessage("Description is too long.");
        
        RuleFor(m => m.Rating).InclusiveBetween(0, 10).WithMessage("Rating must be between 0 and 10.");
        
        RuleFor(m => m.AgeLimit).NotEmpty().WithMessage("Age limit is required.")
            .IsInEnum().WithMessage("AgeLimit must be a valid value from the AgeLimitEnum enum");
        
        RuleFor(c => c.ReleaseDate)
            .Must(be => be <= DateTime.Now).WithMessage("PublishedAt must be a date in the past");    }
}