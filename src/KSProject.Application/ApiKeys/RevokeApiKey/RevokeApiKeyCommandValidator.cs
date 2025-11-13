using FluentValidation;

namespace KSProject.Application.ApiKeys.RevokeApiKey;

public sealed class RevokeApiKeyCommandValidator : AbstractValidator<RevokeApiKeyCommand>
{
    public RevokeApiKeyCommandValidator()
    {
        RuleFor(c => c.Payload.UserId)
            .NotEmpty()
            .WithMessage("UserId cannot be empty.");

        RuleFor(c => c.Payload.ApiKeyId)
            .NotEmpty()
            .WithMessage("ApiKeyId cannot be empty.");
    }
}
