using FluentValidation;
using KSFramework.Utilities;
using KSProject.Common.Constants.Messages;

namespace KSProject.Application.Users.UpdateUser;

public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
	public UpdateUserCommandValidator()
	{
		RuleFor(u => u.Payload.UserName)
			.NotEmpty()
			.WithMessage(ValidationMessages.ShouldNotBeEmptyOrWhiteSpace)
			.NotNull()
			.WithMessage(ValidationMessages.ShouldNotBeNull)
			.Length(5, 50)
			.WithMessage(ValidationMessages.LengthShouldBeBetween(5, 50));

		RuleFor(u => u.Payload.Email)
			.EmailAddress()
			.WithMessage(ValidationMessages.InvalidEmailFormat)
			.NotEmpty()
			.Must(x => x.IsValidEmail())
			.WithMessage(ValidationMessages.ShouldNotBeEmptyOrWhiteSpace)
			.NotNull()
			.WithMessage(ValidationMessages.ShouldNotBeNull);

		RuleFor(u => u.Payload.PhoneNumber)
			.NotEmpty()
			.WithMessage(ValidationMessages.ShouldNotBeEmptyOrWhiteSpace)
			.NotNull()
			.WithMessage(ValidationMessages.ShouldNotBeNull)
			.Length(10, 20)
			.WithMessage(ValidationMessages.LengthShouldBeBetween(10, 20))
			.Must(x => x.IsValidMobile())
			.WithMessage(ValidationMessages.InvalidPhoneNumber);

		RuleFor(u => u.Payload.Roles)
			.NotEmpty()
			.WithMessage(ValidationMessages.TheListCouldNotBeEmpty)
			.NotNull()
			.WithMessage(ValidationMessages.TheListCouldNotBeNull)
			.Must(r => r.Length > 0);
	}
}