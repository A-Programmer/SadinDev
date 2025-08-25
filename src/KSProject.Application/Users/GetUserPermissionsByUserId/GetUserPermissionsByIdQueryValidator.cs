using FluentValidation;

namespace KSProject.Application.Users.GetUserPermissionsByUserId;
public sealed class GetUserPermissionsByIdQueryValidator : AbstractValidator<GetUserPermissionsByIdQuery>
{
	public GetUserPermissionsByIdQueryValidator()
	{
		RuleFor(x => x.Payload.id)
			.NotNull()
			.NotEmpty();
	}
}
