using KSFramework.KSMessaging.Abstraction;
using KSFramework.Pagination;
using KSProject.Domain.Aggregates.Roles;
using KSProject.Domain.Contracts;

namespace KSProject.Application.Admin.Roles.GetPaginatedRoles;

public sealed class GetPaginatedRolesQueryHandler
    : IQueryHandler<GetPaginatedRolesQuery,
        PaginatedList<RolesListItemResponse>>
{
    private readonly IKSProjectUnitOfWork _uow;

    public GetPaginatedRolesQueryHandler(IKSProjectUnitOfWork uow)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
    }

    public async Task<PaginatedList<RolesListItemResponse>> Handle(GetPaginatedRolesQuery request, CancellationToken cancellationToken)
    {
        PaginatedList<Role> roles = await _uow.Roles.GetPagedAsync(
            request.PageIndex,
            request.PageSize,
            request.Where,
            request.OrderByPropertyName,
            request.Desc);
        
        return new PaginatedList<RolesListItemResponse>(roles
                .Select(r => new RolesListItemResponse(r.Id, r.Name, r.Description))
                .ToList(),
            roles.Count,
            roles.PageIndex,
            request.PageSize);
    }
}
