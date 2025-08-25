using FluentValidation;
using KSProject.Common.Constants.Messages;

namespace KSProject.Application.Users.GetUserById;

public sealed class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(x => x.Payload.id)
            .NotNull()
            .WithMessage(ValidationMessages.ShouldNotBeNull)
            .NotEmpty()
            .WithMessage(ValidationMessages.ShouldNotBeEmptyOrWhiteSpace);
    }
}