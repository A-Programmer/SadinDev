using System.Linq.Expressions;
using KSFramework.KSMessaging.Abstraction;
using KSFramework.Pagination;
using KSFramework.Utilities;
using KSProject.Domain.Aggregates.Users;

namespace KSProject.Application.Users.GetPaginatedUsers;

public record GetPaginatedUsersQuery
    : IQuery<PaginatedList<UsersListItemResponse>>
{
    private readonly GetPaginatedUsersRequest _payload;
    public GetPaginatedUsersQuery(GetPaginatedUsersRequest payload)
    {
        if(payload.SearchTerm.HasValue())
        {
            Where = x =>
                x.Id.ToString().ToLower() == payload.SearchTerm.ToLower() ||
                x.UserName.ToLower().Contains(payload.SearchTerm.ToLower()) ||
                x.Email.ToLower().Contains(payload.SearchTerm.ToLower()) ||
                x.Roles.Any(r => r.Name.ToLower() == payload.SearchTerm.ToLower()) ||
                x.PhoneNumber.ToLower().Contains(payload.SearchTerm.ToLower());
        }
        OrderByPropertyName = payload.OrderByPropertyName.HasValue() ?  payload.OrderByPropertyName : "Id";
        Desc = payload.Desc;
        PageIndex = payload.PageIndex;
        PageSize = payload.PageSize;
    }
    
    public int PageIndex { get; private set; }
    public int PageSize { get; private set; }
    public Expression<Func<User, bool>>? Where { get; private set; }
    public string OrderByPropertyName { get; private set; }
    public bool Desc { get; private set; }
}