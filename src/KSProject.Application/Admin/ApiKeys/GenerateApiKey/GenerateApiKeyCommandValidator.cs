using FluentValidation;

namespace KSProject.Application.Admin.ApiKeys.GenerateApiKey;

public sealed class GenerateApiKeyCommandValidator : AbstractValidator<GenerateApiKeyCommand>
{
    public GenerateApiKeyCommandValidator()
    {
        RuleFor(c => c.Payload.UserId)
            .NotEmpty()
            .WithMessage("UserId cannot be empty.");
    }
}
