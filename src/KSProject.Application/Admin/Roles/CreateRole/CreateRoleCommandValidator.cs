using FluentValidation;

namespace KSProject.Application.Admin.Roles.CreateRole;

public sealed class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(r => r.Payload.Name)
            .NotEmpty()
            .NotNull()
            .Length(3, 50);
        
        RuleFor(r => r.Payload.Description)
            .NotEmpty()
            .NotNull();
    }
}
