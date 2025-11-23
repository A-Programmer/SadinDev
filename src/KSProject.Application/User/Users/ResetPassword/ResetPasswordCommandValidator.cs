using FluentValidation;
using KSProject.Common.Constants.Messages;

namespace KSProject.Application.User.Users.ResetPassword;

public sealed class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(u => u.Payload.NewPassword)
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
