using FluentValidation;

namespace KSProject.Application.Admin.Roles.GetRolePermissionsByRoleId;
public sealed class GetRolePermissionsQueryValidator : AbstractValidator<GetRolePermissionsQuery>
{
	public GetRolePermissionsQueryValidator()
	{
		RuleFor(x => x.Payload.id).NotEmpty().WithMessage("Role Id cannot be empty.");
	}
}
