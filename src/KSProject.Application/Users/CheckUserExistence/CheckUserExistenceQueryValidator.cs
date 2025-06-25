using FluentValidation;
using KSProject.Common.Constants.Messages;

namespace KSProject.Application.Users.CheckUserExistence;

public sealed class CheckUserExistenceQueryValidator : AbstractValidator<CheckUserExistenceQuery>
{
    public CheckUserExistenceQueryValidator()
    {
        RuleFor(x => x.Payload.UserNameOrEmailOrPhoneNumber)
            .NotNull()
            .WithMessage(ValidationMessages.ShouldNotBeNull)
            .NotEmpty()
            .WithMessage(ValidationMessages.ShouldNotBeEmptyOrWhiteSpace)
            .MinimumLength(5)
            .WithMessage(ValidationMessages.MinimumLengthError(5));
    }
}