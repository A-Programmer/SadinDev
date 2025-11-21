using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Admin.Users.UpdateUser;

public sealed record UpdateUserCommand(
	UpdateUserRequest Payload
	)
	: ICommand<UpdateUserResponse>;
