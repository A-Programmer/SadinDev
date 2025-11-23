using FluentValidation;
using KSProject.Common.Constants.Messages;

namespace KSProject.Application.Admin.Users.DeleteUser;

public sealed class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.Payload.id)
            .NotNull()
            .WithMessage(ValidationMessages.ShouldNotBeNull)
            .NotEmpty()
            .WithMessage(ValidationMessages.ShouldNotBeEmptyOrWhiteSpace);
    }
}
