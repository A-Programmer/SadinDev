using FluentValidation;

namespace KSProject.Application.Roles.GetRoleById;

public sealed class GetRoleByIdQueryValidator : AbstractValidator<GetRoleByIdQuery>
{
    public GetRoleByIdQueryValidator()
    {
        RuleFor(r => r.Payload.id)
            .NotEmpty()
            .NotNull();
    }
}