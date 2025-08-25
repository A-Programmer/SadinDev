using KSFramework.Exceptions;
using KSFramework.GenericRepository;
using KSFramework.KSMessaging.Abstraction;
using KSProject.Domain.Aggregates.Users;
using KSProject.Domain.Contracts;

namespace KSProject.Application.Users.GetUserById;

public sealed class GetUserByIdQueryHandler
    : IQueryHandler<GetUserByIdQuery,
        UserResponse>
{
    private readonly IKSProjectUnitOfWork _uow;

    public GetUserByIdQueryHandler(IKSProjectUnitOfWork uow)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
    }
    
    public async Task<UserResponse> Handle(GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        User? user = await _uow.Users.FindUserWithRolesAsync(request.Payload.id, cancellationToken);

        if (user is null)
            throw new KSNotFoundException(request.Payload.id.ToString());

        return new UserResponse
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Roles = user.Roles.Select(r => r.Name).ToList()
        };
    }
}