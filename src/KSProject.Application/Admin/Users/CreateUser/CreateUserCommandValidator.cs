using FluentValidation;
using KSProject.Common.Constants.Messages;

namespace KSProject.Application.Admin.Users.CreateUser;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(u => u.Payload.UserName)
            .NotEmpty()
            .WithMessage(ValidationMessages.ShouldNotBeEmptyOrWhiteSpace)
            .NotNull()
            .WithMessage(ValidationMessages.ShouldNotBeNull)
            .Length(5, 16)
            .WithMessage(ValidationMessages.LengthShouldBeBetween(5, 16));

        RuleFor(u => u.Payload.Email)
            .NotEmpty()
            .WithMessage(ValidationMessages.InvalidEmail)
            .NotNull()
            .WithMessage(ValidationMessages.InvalidEmail)
            .EmailAddress();

        RuleFor(u => u.Payload.PhoneNumber)
            .NotEmpty()
            .WithMessage(ValidationMessages.InvalidPhoneNumber)
            .NotNull()
            .WithMessage(ValidationMessages.InvalidPhoneNumber);

        RuleFor(u => u.Payload.Password)
            .NotEmpty()
            .WithMessage(ValidationMessages.ShouldNotBeEmpty)
            .NotNull()
            .WithMessage(ValidationMessages.ShouldNotBeNull)
            .Length(6,32)
            .WithMessage(ValidationMessages.LengthShouldBeBetween(6, 32))
            .Equal(u => u.Payload.ConfirmPassword)
            .WithMessage(ValidationMessages.PasswordAndConfirmationAreNotEqual);

        RuleFor(u => u.Payload.Roles)
            .NotEmpty()
            .WithMessage(ValidationMessages.TheListCouldNotBeEmpty)
            .NotNull()
            .WithMessage(ValidationMessages.TheListCouldNotBeNull)
            .Must(x => x.Length > 0);
    }
}
