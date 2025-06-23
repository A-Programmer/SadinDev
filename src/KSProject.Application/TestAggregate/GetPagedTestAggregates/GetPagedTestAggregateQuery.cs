using System.Linq.Expressions;
using KSFramework.KSMessaging.Abstraction;
using KSFramework.Pagination;
using KSFramework.Utilities;
using KSProject.Common;

namespace KSProject.Application.TestAggregate.GetPagedTestAggregates;

public record GetPagedTestAggregateQuery : IQuery<PaginatedList<GetPagedTestAggregateResponse>>
{
    private readonly GetPagedTestAggregateRequest _payload;
    public GetPagedTestAggregateQuery(GetPagedTestAggregateRequest payload)
    {
        _payload = payload;
        if(_payload.SearchTerm.HasValue())
        {
            Where = x =>
                x.Id.ToString().ToLower() == _payload.SearchTerm.ToLower() ||
                x.Title.ToLower().Contains(_payload.SearchTerm.ToLower()) ||
                x.Content.ToLower().Contains(_payload.SearchTerm.ToLower());
        }
        OrderByPropertyName = _payload.OrderByPropertyName.HasValue() ?  _payload.OrderByPropertyName : "Id";
        Desc = _payload.Desc;
        PageIndex = _payload.PageIndex;
        PageSize = _payload.PageSize;
    }

    public int PageIndex { get; private set; }
    public int PageSize { get; private set; }
    public Expression<Func<Domain.Aggregates.Tes.TestAggregate, bool>>? Where { get; private set; }
    public string OrderByPropertyName { get; private set; }
    public bool Desc { get; private set; }
}