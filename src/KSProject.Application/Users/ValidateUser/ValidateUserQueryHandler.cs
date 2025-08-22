using KSFramework.KSMessaging.Abstraction;
using KSFramework.Utilities;
using KSProject.Domain.Aggregates.Users;
using KSProject.Domain.Contracts;

namespace KSProject.Application.Users.ValidateUser;

public sealed class ValidateUserQueryHandler :
	IQueryHandler<ValidateUserQuery, ValidateUserQueryResponse?>
{
	private readonly IKSProjectUnitOfWork _uow;
	public ValidateUserQueryHandler(IKSProjectUnitOfWork uow)
	{
		_uow = uow ?? throw new ArgumentNullException(nameof(uow));
	}

	public async Task<ValidateUserQueryResponse?> Handle(ValidateUserQuery request,
		CancellationToken cancellationToken)
	{
		string hashedPassword = SecurityHelper.GetSha256Hash(request.Payload.Password);
		User? user = await _uow.Users.FindUserByUserNameAsync(request.Payload.UserName,
			cancellationToken);

		if (user is not null && user.HashedPassword == hashedPassword)
		{
			return new ValidateUserQueryResponse(user.Id,
				user.UserName,
				user.SuperAdmin,
				user.Active);
		}
		else
		{
			user = await _uow.Users.FindUserByEmailAsync(request.Payload.UserName,
			cancellationToken);
			if (user is not null && user.HashedPassword == hashedPassword)
			{
				var test = new ValidateUserQueryResponse(user.Id,
					user.UserName,
					user.SuperAdmin,
					user.Active);
				return test;
			}
			else
			{
				user = await _uow.Users.FindUserByPhoneNumberAsync(request.Payload.UserName,
				cancellationToken);
				if (user is not null && user.HashedPassword == hashedPassword)
				{
					return new ValidateUserQueryResponse(user.Id,
						user.UserName,
						user.SuperAdmin,
						user.Active);
				}
			}
		}
		return null;
	}
}
