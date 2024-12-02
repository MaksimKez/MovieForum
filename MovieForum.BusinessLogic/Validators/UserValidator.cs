using FluentValidation;
using MovieForum.BusinessLogic.Models;

namespace MovieForum.BusinessLogic.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(u => u.Username)
            .NotEmpty().WithMessage("Username is required")
            .Length(3, 20).WithMessage("Username must be between 3 and 20 characters");

        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email address");

        RuleFor(u => u.PasswordHash)
            .NotEmpty().When(u => string.IsNullOrEmpty(u.GoogleId)).WithMessage("Password is required");

        //RuleFor(u => u.PasswordSalt)
            //.NotEmpty().When(u => string.IsNullOrEmpty(u.GoogleId)).WithMessage("Password salt is required");

        RuleFor(u => u.GoogleId).NotEmpty()
            .When(u => string.IsNullOrEmpty(u.PasswordHash) && string.IsNullOrEmpty(u.PasswordSalt))
            .WithMessage("Google ID is required");

        RuleFor(u => u.FullName).NotEmpty()
            .When(u => string.IsNullOrEmpty(u.PasswordHash) && string.IsNullOrEmpty(u.PasswordSalt))
            .WithMessage("Full name is required");
    }
}