using FluentValidation;
using KSProject.Common.Constants.Messages;

namespace KSProject.Application.User.Users.Login;

public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(l => l.Payload.UserName)
            .NotEmpty()
            .WithMessage(ValidationMessages.ShouldNotBeEmptyOrWhiteSpace)
            .NotNull()
            .WithMessage(ValidationMessages.ShouldNotBeEmptyOrWhiteSpace)
            .Length(5, 16)
            .WithMessage(ValidationMessages.LengthShouldBeBetween(5, 16));
        
        RuleFor(l => l.Payload.Password)
            .NotEmpty()
            .WithMessage(ValidationMessages.ShouldNotBeEmptyOrWhiteSpace)
            .NotNull()
            .WithMessage(ValidationMessages.ShouldNotBeEmptyOrWhiteSpace)
            .Length(5, 16)
            .WithMessage(ValidationMessages.LengthShouldBeBetween(6, 32));
    }
}
