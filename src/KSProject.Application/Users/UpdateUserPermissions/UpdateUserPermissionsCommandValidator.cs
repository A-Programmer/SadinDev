using FluentValidation;

namespace KSProject.Application.Users.UpdateUserPermissions;
public sealed class UpdateUserPermissionsCommandValidator : AbstractValidator<UpdateUserPermissionsCommand>
{
	public UpdateUserPermissionsCommandValidator()
	{
		RuleFor(x => x.Payload.Id)
			.NotEmpty().WithMessage("Id is required.");

		RuleFor(x => x.Payload.Permissions)
			.NotNull().WithMessage("Permissions are required.")
			.Must(p => p != null && p.All(permission => !string.IsNullOrWhiteSpace(permission)))
			.WithMessage("Permissions cannot contain null or empty values.");
	}
}
