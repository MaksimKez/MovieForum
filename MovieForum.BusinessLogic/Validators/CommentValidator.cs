using FluentValidation;
using MovieForum.BusinessLogic.Models;
using MovieForum.Data.Interfaces;

namespace MovieForum.BusinessLogic.Validators;

public class CommentValidator : AbstractValidator<Comment>
{
    public CommentValidator()
    {
        RuleFor(c => c.Text)
            .NotEmpty().WithMessage("Text is required")
            .Length(1, 1000).WithMessage("Text must be between 1 and 1000 characters");
        
        RuleFor(c => c.PublishedAt)
            .Must(be => be <= DateTime.Now).WithMessage("PublishedAt must be a date in the past");

        RuleFor(c => c.UserId)
            .NotEmpty().WithMessage("UserId is required");

        RuleFor(c => c.ReviewId)
            .NotEmpty().WithMessage("ReviewId is required");
    }
}