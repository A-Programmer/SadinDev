using KSFramework.KSMessaging.Abstraction;
using KSFramework.Pagination;
using KSProject.Domain.Aggregates.Users;
using KSProject.Domain.Contracts;

namespace KSProject.Application.Users.GetPaginatedUsers;

public sealed class GetPaginatedUsersQueryHandler
    : IQueryHandler<GetPaginatedUsersQuery,
        PaginatedList<UsersListItemResponse>>
{
    private readonly IKSProjectUnitOfWork _uow;

    public GetPaginatedUsersQueryHandler(IKSProjectUnitOfWork uow)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
    }

    public async Task<PaginatedList<UsersListItemResponse>> Handle(
        GetPaginatedUsersQuery request,
        CancellationToken cancellationToken)
    {
        PaginatedList<User> users = await _uow.Users.GetPaginatedUsersWithRolesAsync(request.PageIndex,
            request.PageSize,
            request.Where,
            request.OrderByPropertyName,
            request.Desc,
            cancellationToken);

        return new PaginatedList<UsersListItemResponse>(users
                .Select(u => new UsersListItemResponse()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Roles = u.Roles.Select(r => r.Name).ToList()
                })
                .ToList(),
            users.Count,
            users.PageIndex,
            request.PageSize);
    }
}