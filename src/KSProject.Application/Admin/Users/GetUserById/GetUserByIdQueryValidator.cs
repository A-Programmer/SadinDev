using FluentValidation;
using KSProject.Common.Constants.Messages;

namespace KSProject.Application.Admin.Users.GetUserById;

public sealed class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull()
            .WithMessage(ValidationMessages.ShouldNotBeNull)
            .NotEmpty()
            .WithMessage(ValidationMessages.ShouldNotBeEmptyOrWhiteSpace);
    }
}
