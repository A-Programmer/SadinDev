using FluentValidation;

namespace KSProject.Application.Admin.Users.GetUserPermissionsByUserId;
public sealed class GetUserPermissionsByIdQueryValidator : AbstractValidator<GetUserPermissionsByIdQuery>
{
	public GetUserPermissionsByIdQueryValidator()
	{
		RuleFor(x => x.Payload.id)
			.NotNull()
			.NotEmpty();
	}
}
