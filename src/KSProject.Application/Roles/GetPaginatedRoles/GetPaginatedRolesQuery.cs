using System.Linq.Expressions;
using KSFramework.KSMessaging.Abstraction;
using KSFramework.Pagination;
using KSFramework.Utilities;
using KSProject.Domain.Aggregates.Roles;
using Sadin.Application.Roles.GetPaginatedRoles;

namespace KSProject.Application.Roles.GetPaginatedRoles;

public class GetPaginatedRolesQuery
    : IQuery<PaginatedList<RolesListItemResponse>>
{
    private readonly GetPaginatedRolesRequest _payload;
    public GetPaginatedRolesQuery(GetPaginatedRolesRequest payload)
    {
        _payload = payload;
        if(payload.SearchTerm.HasValue())
        {
            Where = x =>
                x.Name.ToLower().Contains(payload.SearchTerm.ToLower()) ||
                x.Description.ToLower().Contains(payload.SearchTerm.ToLower());
        }
        OrderByPropertyName = payload.OrderByPropertyName.HasValue() ?  payload.OrderByPropertyName : "Id";
        Desc = payload.Desc;
        PageIndex = payload.PageIndex;
        PageSize = payload.PageSize;
    }
    
    public int PageIndex { get; private set; }
    public int PageSize { get; private set; }
    public Expression<Func<Role, bool>>? Where { get; private set; }
    public string OrderByPropertyName { get; private set; }
    public bool Desc { get; private set; }
}