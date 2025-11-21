using KSFramework.Exceptions;
using KSFramework.KSMessaging.Abstraction;
using KSProject.Domain.Contracts;

namespace KSProject.Application.Admin.Users.GetUserById;

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
        Domain.Aggregates.Users.User? user = await _uow.Users.FindUserWithRolesAsync(request.UserId, cancellationToken);

        if (user is null)
            throw new KSNotFoundException(request.UserId.ToString());

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
