using FluentValidation;
using KSProject.Common.Constants.Messages;

namespace KSProject.Application.Users.Register;

public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
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
    }
}