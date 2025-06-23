using FluentValidation;

namespace KSProject.Application.Roles.DeleteRole;

public sealed class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
{
    public DeleteRoleCommandValidator()
    {
        RuleFor(x => x.Payload.id)
            .NotEmpty()
            .NotNull();
    }
}