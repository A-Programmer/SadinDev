using FluentValidation;

namespace KSProject.Application.Admin.Roles.DeleteRole;

public sealed class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
{
    public DeleteRoleCommandValidator()
    {
        RuleFor(x => x.Payload.id)
            .NotEmpty()
            .NotNull();
    }
}
