using System.Security.Authentication;
using KSFramework.Exceptions;
using KSFramework.KSMessaging.Abstraction;
using KSFramework.Utilities;
using KSProject.Common.Constants.Enums;
using KSProject.Common.Constants.Messages;
using KSProject.Domain.Aggregates.Users.ValueObjects;
using KSProject.Domain.Contracts;

// TODO: The login should return Access Access_Token and Refresh Access_Token
namespace KSProject.Application.User.Users.Login;

public sealed class LoginCommandHandler : ICommandHandler<LoginCommand, LoginResponse>
{
	private readonly IKSProjectUnitOfWork _uow;
	private readonly IJwtService _jwtService;

	public LoginCommandHandler(IKSProjectUnitOfWork uow,
		IJwtService jwtService)
	{
		_uow = uow;
		_jwtService = jwtService;
	}

	public async Task<LoginResponse> Handle(LoginCommand request,
		CancellationToken cancellationToken)
	{
		Domain.Aggregates.Users.User? user;
		user = await _uow.Users.FindUserByUserNameAsync(request.Payload.UserName,
		cancellationToken);
		// TODO: Add Log => User not found for Login
		if (user is null)
		{
			user = await _uow.Users.FindUserByEmailAsync(request.Payload.UserName,
			cancellationToken);
			if (user is null)
			{
				user = await _uow.Users.FindUserByPhoneNumberAsync(request.Payload.UserName,
				cancellationToken);
				if (user is null)
				{
					throw new KSNotFoundException(request.Payload.UserName);
					// TODO: Add Log => User found for Login
				}
			}
		}
		string hashedPassword = SecurityHelper.GetSha256Hash(request.Payload.Password);
		if (user.HashedPassword != hashedPassword)
			throw new AuthenticationException(GeneralMessages.WrongUserNameOrPassword);

		user = await _uow.Users.FindUserIncludingRolesAndPermissionsAsync(user.Id, cancellationToken)
			?? throw new KSNotFoundException(request.Payload.UserName);

		List<string> permissions = user.Permissions.Select(x => x.Name).ToList() ?? new List<string>();

		foreach (var role in user.Roles)
		{
			permissions.AddRange(role.Permissions.Select(x => x.Name).ToList());
		}
		permissions = permissions.Distinct().ToList();

		user.ClearTokensByType(TokenTypes.RefreshToken);
		user.ClearTokensByType(TokenTypes.AccessToken);

		user.ClearSecurityStamps();

		UserSecurityStamp securityStamp = new(
			Guid.NewGuid().ToString(),
            DateTime.UtcNow,
            DateTime.UtcNow.AddDays(30));

		user.AddSecurityStamp(securityStamp);

		UserToken accessToken = new(TokenTypes.AccessToken,
			_jwtService.GenerateToken(user, permissions),
            DateTime.UtcNow.AddDays(1));

		UserToken refreshToken = new(TokenTypes.RefreshToken,
			SecurityHelper.GenerateToken(),
            DateTime.UtcNow.AddDays(7));

		user.AddLoginDate(new(DateTime.UtcNow, request.Payload.IpAddress));

		user.AddToken(accessToken);
		user.AddToken(refreshToken);

		await _uow.SaveChangesAsync(cancellationToken);

		return new LoginResponse
		{
			Access_Token = accessToken.Token,
			Refresh_Token = refreshToken.Token,
			Expire_At = DateTime.UtcNow.AddDays(1)
		};
	}
}
