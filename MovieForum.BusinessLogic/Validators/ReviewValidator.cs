using FluentValidation;
using MovieForum.BusinessLogic.Models;

namespace MovieForum.BusinessLogic.Validators;

public class ReviewValidator : AbstractValidator<Review>
{
    public ReviewValidator()
    {
        RuleFor(r => r.Title).NotEmpty().WithMessage("Title is required.")
            .MaximumLength(50).WithMessage("Title is too long.");
        
        RuleFor(r => r.Text).NotEmpty().WithMessage("Text is required.")
            .MaximumLength(2000).WithMessage("Text is too long.");
        
        RuleFor(m => m.Rating)
            .IsInEnum().WithMessage("Rating must be a valid value from the RatingEnum enum");

        RuleFor(r => r.MovieId).NotEmpty().WithMessage("MovieId is required.");
        RuleFor(r => r.UserId).NotEmpty().WithMessage("UserId is required.");
    }
    
}