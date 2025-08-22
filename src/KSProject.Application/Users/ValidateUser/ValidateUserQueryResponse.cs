namespace KSProject.Application.Users.ValidateUser;

public record ValidateUserQueryResponse(Guid Id,
	string UserName,
	bool SuperAdmin,
	bool IsActive);
