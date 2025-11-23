using FluentValidation;

namespace KSProject.Application.Admin.Roles.UpdateRole;

public sealed class RoleUpdateCommandValidator : AbstractValidator<RoleUpdateCommand>
{
    public RoleUpdateCommandValidator()
    {
        RuleFor(r => r.Payload.Id)
            .NotNull()
            .NotEmpty();

        RuleFor(r => r.Payload.Name)
            .NotEmpty()
            .NotNull()
            .Length(3, 5);

        RuleFor(r => r.Payload.Description)
            .NotNull();
    }
}
