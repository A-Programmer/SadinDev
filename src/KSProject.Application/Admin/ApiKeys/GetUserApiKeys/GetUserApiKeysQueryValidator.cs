using FluentValidation;

namespace KSProject.Application.Admin.ApiKeys.GetUserApiKeys;

public sealed class GetUserApiKeysQueryValidator : AbstractValidator<GetUserApiKeysQuery>
{
    public GetUserApiKeysQueryValidator()
    {
        RuleFor(q => q.Payload.UserId)
            .NotEmpty()
            .WithMessage("UserId cannot be empty.");
    }
}
