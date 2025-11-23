using FluentValidation;

namespace KSProject.Application.Shared.Users.ValidateUser
{
	public sealed class ValidateUserQueryValidator : AbstractValidator<ValidateUserQuery>
	{
		public ValidateUserQueryValidator()
		{
			RuleFor(u => u.Payload.UserName)
				.NotEmpty()
				.NotNull();

			RuleFor(p => p.Payload.Password)
				.NotEmpty()
				.NotNull();
		}
	}
}
