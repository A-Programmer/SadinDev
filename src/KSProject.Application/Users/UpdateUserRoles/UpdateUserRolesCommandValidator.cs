using FluentValidation;

namespace KSProject.Application.Users.UpdateUserRoles;

public sealed class UpdateUserRolesCommandValidator : AbstractValidator<UpdateUserRolesCommand>
{
    public UpdateUserRolesCommandValidator()
    {
        RuleFor(ur => ur.Payload.Roles)
            .Must(r => r.Length > 0);
    }
}