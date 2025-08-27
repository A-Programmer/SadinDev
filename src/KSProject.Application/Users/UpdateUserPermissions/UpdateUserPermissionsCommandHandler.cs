using KSFramework.Exceptions;
using KSFramework.KSMessaging.Abstraction;
using KSProject.Domain.Aggregates.Users;
using KSProject.Domain.Contracts;

namespace KSProject.Application.Users.UpdateUserPermissions;
public sealed class UpdateUserPermissionsCommandHandler : ICommandHandler<UpdateUserPermissionsCommand>
{
	private readonly IKSProjectUnitOfWork _uow;
	private readonly IJwtService _jwtService;

	public UpdateUserPermissionsCommandHandler(
		IKSProjectUnitOfWork uow,
		IJwtService jwtService)
	{
		_uow = uow ?? throw new ArgumentNullException(nameof(uow));
		_jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
	}

	public async Task<Unit> Handle(UpdateUserPermissionsCommand request, CancellationToken cancellationToken)
	{
		User user = (await _uow.Users.GetUserAndPermissionsAsync(request.Payload.Id,
			cancellationToken)) ??
			 throw new KSNotFoundException(request.Payload.Id.ToString());

		user.UpdatePermissions(request.Payload.Id, request.Payload.Permissions);

		_uow.Users.Update(user);

		await _uow.SaveChangesAsync();
		return Unit.Value;
	}
}
