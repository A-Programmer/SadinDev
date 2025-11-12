using KSFramework.Exceptions;
using KSFramework.KSMessaging.Abstraction;
using KSFramework.Utilities;
using KSProject.Common.Constants.Enums;
using KSProject.Common.Exceptions;
using KSProject.Domain.Aggregates.Roles;
using KSProject.Domain.Aggregates.Users;
using KSProject.Domain.Aggregates.Users.ValueObjects;
using KSProject.Domain.Contracts;

namespace KSProject.Application.Users.UpdateUser;

public sealed class UpdateUserCommandHandler
	: ICommandHandler<UpdateUserCommand,
		UpdateUserResponse>
{
	private readonly IKSProjectUnitOfWork _uow;
	private readonly IJwtService _jwtService;

	public UpdateUserCommandHandler(IKSProjectUnitOfWork uow,
		IJwtService jwtService)
	{
		_uow = uow ?? throw new ArgumentNullException(nameof(uow));
		_jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
	}

	public async Task<UpdateUserResponse> Handle(UpdateUserCommand request,
		CancellationToken cancellationToken)
	{
		User user = (await _uow.Users.FindUserWithRolesAsync(request.Payload.Id,
			cancellationToken)) ??
					 throw new KSNotFoundException(request.Payload.UserName);

		if (await AreInformationInUse(request, cancellationToken))
			throw new KsDuplicatedUserException("A user with the same information exist.");

		user = await _uow.Users.FindUserWithRolesAsync(user.Id, cancellationToken)
	?? throw new KSNotFoundException(request.Payload.UserName);

		List<string> permissions = user.Permissions.Select(x => x.Name).ToList() ?? new List<string>();

		foreach (var role in user.Roles)
		{
			permissions.AddRange(role.Permissions.Select(x => x.Name).ToList());
		}
		permissions = permissions.Distinct().ToList();

		user.ClearSecurityStamps();
		user.ClearTokens();

		UserSecurityStamp securityStamp = new(SecurityHelper.GenerateToken(), DateTime.UtcNow, DateTime.UtcNow.AddDays(30));

		UserToken accessToken = new(TokenTypes.AccessToken, _jwtService.GenerateToken(user, permissions), DateTime.UtcNow.AddDays(7));
		UserToken refreshToken = new(TokenTypes.RefreshToken, SecurityHelper.GenerateToken(), DateTime.UtcNow.AddDays(30));
		// We might need these two tokens if the user needs to confirm their email and phone number
		// Then we need to send confirmation link to the email and confirmation code via SMS to the user
		UserToken emailConfirmationToken = new(TokenTypes.EmailConfirmationToken, SecurityHelper.GenerateToken(), DateTime.UtcNow.AddMinutes(30));
		UserToken phoneConfirmationToken = new(TokenTypes.PhoneConfirmationToken, SecurityHelper.GenerateToken(), DateTime.UtcNow.AddMinutes(30));

		user.AddToken(accessToken);
		user.AddToken(refreshToken);
		user.AddToken(emailConfirmationToken);
		user.AddToken(phoneConfirmationToken);

		user.Update(
			request.Payload.UserName,
			request.Payload.Email,
			request.Payload.PhoneNumber,
			request.Payload.Active,
			securityStamp);

		List<Role> roles = new();

		foreach (string roleName in request.Payload.Roles)
		{
			Role? role = await _uow.Roles.GetByRoleName(roleName,
				cancellationToken) ?? throw new KSNotFoundException(roleName);

			roles.Add(role);
		}

		user.ClearRoles();
		user.AssignRoles(roles);

		await _uow.SaveChangesAsync();

		return new UpdateUserResponse
		{
			Id = request.Payload.Id
		};
	}

	private async Task<bool> AreInformationInUse(UpdateUserCommand request,
		CancellationToken cancellationToken = default)
	{
		return await _uow
			.Users
			.IsEmailInUseAsync(
				request.Payload.Id,
				request.Payload.Email,
				cancellationToken) ||

		await _uow
				.Users.IsPhoneNumberInUseAsync(
					request.Payload.Id,
					request.Payload.PhoneNumber,
					cancellationToken) ||

			await _uow
				.Users.IsUserNameInUseAsync(
					request.Payload.Id,
					request.Payload.UserName,
					cancellationToken);
	}
}
